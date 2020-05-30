using Godot;
using System;

public class EnemyBaseDamage : Area2D
{
    protected Root root;
    protected Creature creature;
    protected int damage = 1000;

    public override void _Ready()
    {
        root = (Root)GetNode("/root/root/");
        creature = (Creature)this.GetParent();
    }

    public void _on_DamageArea_body_entered(Node2D body)
    {
        if (body == root.activePlayer && body != null && !creature.GetDied())
        {
            try
            {
                ((ACreature)body).DamageSoul(damage);
            }
            catch {}
        }
    }

    public override void _Process(float delta) // ?? attack more then once ??
    {

    }

}
