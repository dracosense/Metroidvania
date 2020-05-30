using Godot;
using System;

public class SpiritCreature : ACreature
{

    protected int soul;
    protected int maxSoul;

    protected virtual void OnDestroyEvent()
    {
        
    }

    protected void DeathCheck()
    {
        if (soul <= 0)
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

     protected void SetSoulColor()
     {
         Color c = sprite.Modulate;
         c.a = 0.8f * ((float)soul / maxSoul) + 0.2f;
        sprite.Modulate = c;
     }

    public override void Regenerate()
    {
        base.Regenerate();
        soul = maxSoul;
    }

    public override void SacrificeSoul(int damage)
    {
        soul = Mathf.Max(soul - damage, 0);
    }

    public override void DamageSoul(int damage)
    {
        if (!outOfGame)
        {
            SacrificeSoul(damage);
        }
        base.DamageSoul(damage);
    }
    
    public float GetSoul()
    {
        return soul;
    }

    public override void _Ready()
    {
        maxSoul = 10000;
        base._Ready();
        formMask = 2;
    }

    public override void PhysicsProcess(float delta)
    {
        SetSoulColor();
        DeathCheck();
        base.PhysicsProcess(delta);
    }
}
