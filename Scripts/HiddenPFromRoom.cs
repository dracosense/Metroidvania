using Godot;
using System;
using System.Collections.Generic;

public class HiddenPFromRoom : StaticBody2D
{

    protected Root root;
    protected uint cLayer = 0;
    protected int formMask = 0;
    protected List<int> p;

    public override void _Ready()
    {
        root = (Root)GetNode("/root/root/");
        cLayer = CollisionLayer;
        p = Functions.GetParamsFromName(this.Name);
        if (p.Count > 0)
        {
            formMask = p[0];
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if ((formMask & (1 << root.GetActiveForm())) != 0)
        {
            Visible = false;
            CollisionLayer = 0;
        }
        else
        {
            Visible = true;
            CollisionLayer = cLayer;
        }
    }    
}
