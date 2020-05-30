using Godot;
using System;

public class DamageSAreaW : PMoveAreaW
{
    
    protected Vector2 pushFrom;
    protected float sTime = 0;
    protected float push = 0;
    protected int damage = 0;

    public void DamageInit(int _damage, float _sTime, float _push,  Vector2 _pushFrom)
    {
        pushFrom = _pushFrom;
        sTime = _sTime;
        damage = _damage;
        push = _push;
    }

    public override void DamageCreature(Creature c)
    {
        try
        {
            c.DamageSoul(damage);
            c.SetStunned(sTime);
            c.MoveAndSlide(push * Functions.NormV(c.Position - pushFrom), c.GetGravity());
        }
        catch {}
    }

    public override void DamageDBlock(DestroyableBlock b)
    {
        b.Damage(damage);
    }

    public DamageSAreaW()
    {
        pushFrom = Vector2.Zero;
        push = 0;
    }

    public override void _Ready()
    {
        base._Ready();
    }
}
