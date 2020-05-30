using Godot;
using System;

public class Door : Node2D
{

    protected Root root;
    protected AnimatedSprite sprite;
    protected StaticBody2D obj;
    protected uint cLayer = 0;
    protected uint cMask = 0;
    protected bool playerInside;
    protected bool opened;
    public override void _Ready()
    {
        root = (Root)GetNode("/root/root");
        sprite = (AnimatedSprite)GetNode("ASprite");
        obj = (StaticBody2D)GetNodeOrNull("DoorObj");
        if (obj != null)
        {
            cLayer = obj.CollisionLayer;
            cMask = obj.CollisionMask;
        }
        playerInside = false;
        Close();
    }

    public void Open()
    {
        opened = true;
        sprite.Animation = "opened";
        if (obj != null)
        {
            obj.CollisionLayer = 0;
            obj.CollisionMask = 0;
        }
    }

    public void Close()
    {
        opened = false;
        sprite.Animation = "closed";
        if (obj != null)
        {
            obj.CollisionLayer = cLayer;
            obj.CollisionMask = cMask;
        }
    }

    public void _on_Door_body_entered(Node2D body)
    {
        if (body == root.activePlayer)
        {
            playerInside = true;
        }
    }

    public void _on_Door_body_exited(Node2D body)
    {
        if (body == root.activePlayer)
        {
            playerInside = false;
        }
    }

    public override void  _Process(float delta)
    {
        if (playerInside)
        {
            if (Input.IsActionJustPressed("action"))
            {
                if (opened)
                {
                    Close();
                }
                else
                {
                    Open();
                }
            }
        }
    }
}
