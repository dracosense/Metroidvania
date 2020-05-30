using Godot;
using System;

public class SpellObj : PickableItem
{

    public int spell = 0;
    public int spellForm = 0;

    public void _on_Area_body_entered(Node2D body)
    {
        switch (spellForm)
        {
            case 0:
                try
                {
                    ((MagicPlayer)body).AddSpell(spell);
                }
                catch {}
                break;
            case 1:
                try
                {
                    ((SpiritPlayer)body).AddSpell(spell);    
                }
                catch {}
                break;
            case 2:
                // mind player
                break;
            default:
                break;
        }
        DestroyThisObj();
    }

    public override void _Ready()
    {
        base._Ready();
        if (setFromName)
        {
            if (p.Count >= 3)
            {
                spell = p[2];
            }
            if (p.Count >= 4)
            {
                spellForm = p[3];
            }
        }
    }

}
