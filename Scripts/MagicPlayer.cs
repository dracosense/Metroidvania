using Godot;
using System;
using System.Collections.Generic;

public class MagicPlayer : MagicCreature
{

	public bool active = false;
	public PlayerParam pParam;
	protected Camera2D camera;
	protected MSpell[] spellList;
	protected List<Node> createdSpells;
	protected int attackType;
	protected bool canMove;
    protected bool lastActive = false;

	protected void PlayerAttack()
	{
		int attackKey = PlayerControl.GetAttackNum();
		if (attackKey != -1 && attackKey < Constants.MAGIC_SPELL_LIST_SIZE)
		{
			attacking = true;
			lastActiveAttackTime = spellList[attackType].GetCastTime();
			lastAttackAnimation = spellList[attackType].GetAttackAName();
			if (attackType == attackKey)
			{
				attackTimer = Mathf.Min(attackTimer, lastActiveAttackTime);
			}
			else
			{
				sprite.Frame = 0;
				attackTimer = lastActiveAttackTime;
			}
            attackType = attackKey;
		}
	}

	protected void PlayerActions()
	{
		if (!attacking)
		{
			move = PlayerControl.MoveControl(this);
		}
		else
		{
			move.x = 0;
		}
		PlayerAttack();
	}

	protected void SetActiveFromRoot()
	{
		active = (root.activePlayer == this);
		canMove = active;
		camera.Current = active;
	}

	protected void Attack() // upgrade to crystal magic
	{
		if (!died && useAttack)
		{
			switch(attackType)
			{
				//case 1: // magic sphere
				//	createdSpells.Add(PMWeaponA.SphereA(lastMoveLeft, this));
				//	break;
				default:
					createdSpells.Add(PMWeaponA.HandA(lastMoveLeft, this));
					break;
			}
		}
		useAttack = false;
	}

	public void AddSpell(int s)
	{

	}

    public override void UpdateGameTimers(float delta)
    {
        SetCollisionLayerBit(0, !outOfGame);
        base.UpdateGameTimers(delta);
    }

	public override void Regenerate()
	{
		base.Regenerate();
		for (int i = 0; i < createdSpells.Count; i++)
		{
			try
			{
				createdSpells[i].QueueFree();
			}
			catch {}
		}
		createdSpells.Clear();
	}
	
	public override void _Ready()
	{
        	createdSpells = new List<Node>();
			base._Ready();
			root.magicPlayer = this;
			camera = (Camera2D)GetNode("Camera");
    	    spellList = new MSpell[Constants.MAGIC_SPELL_LIST_SIZE];
			for (int i = 0; i < Constants.MAGIC_SPELL_LIST_SIZE; i++)
			{
				spellList[i] = new MSpell();
			}
			PMWeaponA.LoadRes();
			outGameTime = 3.0f;
	}

	public override void PhysicsProcess(float delta)
	{
		UpdateGameTimers(delta);
		DeathCheck();
		SetActiveFromRoot();
		if (active)
		{
			if (!died && (active == lastActive))
			{
				PlayerActions();
			}
			UpdateAttackTimer(delta);
			Attack();
		}
		else
		{
			attacking = false;
		}
		if ((!attacking || Constants.CAN_ATTACK_WHEN_MOVE) && canMove)
		{
			PhysicsMove(delta, active == lastActive);
		}
		SetAnimationA();
		lastActive = active;
    }
}
