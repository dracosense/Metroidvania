using Godot;
using System;

public class SaveCrystal : Area2D
{

    protected Root root;
    protected bool playerInside;

    public override void _Ready()
    {
        root = (Root)GetNode("/root/root");
        playerInside = false;
    }

    public virtual void _on_Area_body_entered(Node2D body)
    {
        if (body == root.activePlayer)
        {
            playerInside = true;
        }
    }

    public virtual void _on_Area_body_exited(Node2D body)
    {
        if (body == root.activePlayer)
        {
            playerInside = false;
        }
    }

    public override void _Process(float delta)
    {
        if (playerInside)
        {
            if (Input.IsActionJustPressed("action"))
            {
                root.PlayerRegenerate();
                root.SetRebirthPoint(root.activePlayer.Position);
            }
        }
    }

}
