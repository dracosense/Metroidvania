using Godot;
using System;

public class HiddenRoom : Area2D
{

    protected Root root;
    protected StaticBody2D collider;
    protected uint cLayer = 0;
    protected bool hidden = true;

    public void _on_Room_body_entered(Node2D b)
    {
        if (b == root.activePlayer)
        {
            hidden = false;
        }
    }

    public void _on_Room_body_exited(Node2D b)
    {
        if (b == root.activePlayer)
        {
            hidden = true;
        }
    }

    public override void _Ready()
    {
        root = (Root)GetNode("/root/root/"); 
        collider = (StaticBody2D)GetNodeOrNull("Collider");
        if (collider != null)
        {
            cLayer = collider.CollisionLayer;
        }
    }

    public override void _Process(float delta)
    {
        Visible = hidden;
        if (collider != null)
        {
            collider.CollisionLayer = hidden?cLayer:0;
        }
    }

}
