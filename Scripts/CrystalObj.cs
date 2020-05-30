using Godot;
using System;

public class CrystalObj : Area2D
{

    private Root root;

    public int crystal;

    public override void _Ready()
    {
        root = (Root)GetNode("/root/root");
    }

    public void _on_CrystalObj_body_entered(Node2D body)
    {
        root.AddCrystal(crystal);
        QueueFree();
    }

}
