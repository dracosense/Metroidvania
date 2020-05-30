using Godot;
using System;

public class ACreature : Creature
{

    //public const float START_ATTACK_TIME = 0.3f;
    protected string lastAttackAnimation;
    protected float attackTimer = 0;
    protected float lastActiveAttackTime = 0;
    protected bool attacking = false;
    protected bool useAttack = false;

    public override void _Ready()
    {
        base._Ready();
    }

    protected void SetAnimationA()
    {
        if (!attacking)
        {
            SetAnimation();
        }
        else
        {
            sprite.Animation = (lastAttackAnimation == null) ? "attack" : lastAttackAnimation;
            sprite.FlipH = !lastMoveLeft;
        }
    }

    protected void UpdateAttackTimer(float delta)
    {
        if (!died && attacking)
        {
            attackTimer = Mathf.Max(attackTimer - delta, 0);
            if (attackTimer == 0)
            {
                attacking = false;
                useAttack = true;
                attackTimer = lastActiveAttackTime;
            }
        }
        else
        {
            attacking = false;
            attackTimer = lastActiveAttackTime;
        }
    }

    public bool GetAttacking()
    {
        return attacking;
    }

    public void SetAttacking(bool b)
    {
        attacking = b;
    }

    public override void PhysicsProcess(float delta)
    {
        UpdateGameTimers(delta);
        UpdateAttackTimer(delta);
        PhysicsMove(delta, true);
        SetAnimationA();
    }
}
