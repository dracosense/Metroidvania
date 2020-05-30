using Godot;
using System;
using System.Collections.Generic;

public class PickableItem : Area2D
{

    protected Root root;
    protected PhysicsObj parent;
    protected List<int> p; // params
    protected int formMask = 0;
    public float destroyDist = 1200;
    public bool setFromName = true;
    public bool destroyOnDist = false;

    protected void DestroyThisObj()
    {
        if (parent != null)
        {
            parent.QueueFree();
        }
        else
        {
            QueueFree();
        }
    }

    public void SetFMask(int m)
    {
        formMask = m;
        CollisionMask = 0;
        CollisionLayer = 0;
        if (parent != null)
        {
            parent.CollisionMask = 0;
            parent.CollisionLayer = 0;
            parent.formMask = formMask;
        }
        if ((1 & formMask) != 0)
        {
            SetCollisionMaskBit(0, true);
            if (parent != null)
            {
                parent.SetCollisionMaskBit(2, true);
            }
        }
        if ((2 & formMask) != 0)
        {
            SetCollisionMaskBit(5, true);
            if (parent != null)
            {
                parent.SetCollisionMaskBit(7, true);
            }
        }
        if ((4 & formMask) != 0)
        {
            SetCollisionMaskBit(10, true);
            if (parent != null)
            {
                parent.SetCollisionMaskBit(12, true);
            }
        }
    }

    public override void _Ready()
    {
        root = (Root)GetNode("/root/root");
        parent = GetParentOrNull<PhysicsObj>();
        p = new List<int>();
        if (parent == null) // ??
        {
            GD.Print("Pickable item find PhysicsBody2D parent error.");
        }
        if (setFromName)
        {
            if (parent != null)
            {
                p = Functions.GetParamsFromName(parent.Name);
            }
            if (p.Count >= 1)
            {
                SetFMask(p[0]);
            }
            else
            {
                SetFMask(7);
            }
            if (p.Count >= 2)
            {
                destroyOnDist = (p[1] != 0);
            }
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (destroyOnDist && (root.activePlayer == null || Functions.vSqLen(root.activePlayer.Position - this.Position) > destroyDist * destroyDist))
        {
            DestroyThisObj();
        }
    }

}
