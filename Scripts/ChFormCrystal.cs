using Godot;
using System;

public class ChFormCrystal : SaveCrystal
{

    Control talkPanel;
    
    public override void _Ready()
    {
        base._Ready();
        talkPanel = (Control)GetNode("TalkPanel");
    }

    public override void _on_Area_body_entered(Node2D body)
    {
        bool pIn = playerInside;
        base._on_Area_body_entered(body);
        if (pIn != playerInside)
        {
            root.pSaveCNum++;
        }
    }

    public override void _on_Area_body_exited(Node2D body)
    {
        bool pIn = playerInside;
        base._on_Area_body_exited(body);
        if (pIn != playerInside)
        {
            root.pSaveCNum--;
        }
    }

    public override void _Process(float delta)
    {
        if (playerInside)
        {
            talkPanel.Visible = true;
            if (Input.IsActionPressed("talk"))
            {
                //
            }
        }
        else
        {
            talkPanel.Visible = false;
        }
        base._Process(delta);
    }

}
