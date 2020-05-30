using Godot;
using System;

public class Crystal : PickableItem
{

    public int cForm = 0;
    public int cType = 0;

    public void _on_Area_body_entered(Node2D body)
    {
        if (cForm >= 0 && cForm < Constants.CRYSTAL_FORMS_NUM && 
        cType >= 0 && cType < Constants.CRYSTAL_TYPES_NUM)
        {
            root.crystalls[cForm, cType]++;
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
                cForm = p[2];
            }
            if (p.Count >= 4)
            {
                cType = p[3];
            }
        }
    }

}
