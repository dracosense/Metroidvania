using Godot;
using System;

public class MagicCreature : ACreature
{

    protected MagicCParam param;

    protected virtual void OnDestroyEvent()
    {
        
    }

    protected void DeathCheck()
    {
        if (param.soul <= 0)
        {
            if (!died)
            {
                OnDestroyEvent();
                if (sprite != null)
                {
                    sprite.Animation = "death";
                }
            }
            died = true;
        }
    }

    public override void Regenerate()
	{
		base.Regenerate();
		param.soul = param.maxSoul;
	}

    public override void SacrificeSoul(int damage)
    {
        param.soul = Mathf.Max(param.soul - damage, 0);
    }

    public override void DamageSoul(int damage)
    {
        if (!outOfGame)
        {
            SacrificeSoul(damage);
        }
        base.DamageSoul(damage);
    }

    public void DamageParams(Functions.MPStruct p)
    {
        param.p.live -= p.live; 
        param.p.rage -= p.rage;
        param.p.magic -= p.magic;

    }

    public float GetSoul()
    {
        return param.soul;
    }

    public override void _Ready()
    {
        param = new MagicCParam();
        param.maxSoul = 10000;
        base._Ready();
        formMask = 1;
    }

    public override void PhysicsProcess(float delta)
    {
        DeathCheck();
        base.PhysicsProcess(delta);
    }
}
