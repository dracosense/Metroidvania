using Godot;
using System;

public class OnceDamageZone : Area2D
{

    public int soulDamage = 1000;

    public override void _Ready()
    {
        
    }

    public void _on_ODZone_body_entered(Node2D body) // ?? teleport to this zone ??
    {
        try
        {
                Creature c = (Creature)body;
                c.SacrificeSoul(soulDamage);
                c.MoveToLastOnFloor();
        }
        catch {}
    }
}
