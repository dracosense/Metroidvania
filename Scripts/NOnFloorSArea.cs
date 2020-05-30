using Godot;
using System;

public class NOnFloorSArea : Area2D // not on save area
{
    public void _on_Area_body_entered(Node2D obj)
    {
        try
        {
            ((PhysicsObj)obj).saveOnFloorPos++;
        }
        catch {}
    }
    public void _on_Area_body_exited(Node2D obj)
    {
        try
        {
            ((PhysicsObj)obj).saveOnFloorPos--;
        }
        catch {}
    }
}
