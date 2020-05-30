using Godot;
using System;
using System.Collections.Generic;

public class SpiritPlayer : SpiritCreature
{

    public bool active = false;
    protected Camera2D camera;
    protected int attackType;
    protected List<int> spells; //list of spells
    protected List<Node> createdSpells;
    protected bool canMove;
    protected bool lastActive = false;

    protected void PlayerAttack()
    {
        int attackKey = PlayerControl.GetAttackNum();
        if (attackKey != -1 && attackKey < spells.Count)
        {
            attacking = true;
            lastActiveAttackTime = SpiritPSpells.GetCastTime(spells[attackKey]);
            lastAttackAnimation = SpiritPSpells.GetAttackAName(spells[attackKey]);
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

    protected void Attack()
    {
        if (!died && useAttack && attackType < spells.Count)
        {
            switch(spells[attackType])
            {
                case 0:
                    createdSpells.Add(SpiritPSpells.PlatformS(this));
                    SacrificeSoul(3000);
                    break;
                case 1:
                    createdSpells.Add(SpiritPSpells.SwordA(lastMoveLeft, this));
                    SacrificeSoul(40);
                    break;
                case 2:
                    createdSpells.Add(SpiritPSpells.ArrowA(lastMoveLeft, this));
                    SacrificeSoul(40);
                    break;
                default:
                    break;
            }
            // attack
        }
        useAttack = false;
    }

    protected void SetActiveFromRoot()
    {
        active = (root.activePlayer == this);
        canMove = active;
        camera.Current = active;
    }

    public void AddSpell(int s)
    {
        spells.Add(s);
    }

    public override void UpdateGameTimers(float delta)
    {
        SetCollisionLayerBit(5, !outOfGame);
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
        SpiritPSpells.LoadRes();
        root.spiritPlayer = this;
        camera = (Camera2D)GetNode("Camera");
        spells = new List<int>();
        outGameTime = 3.0f;
    }

    public override void PhysicsProcess(float delta)
    {
        UpdateGameTimers(delta);
        SetSoulColor();
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
