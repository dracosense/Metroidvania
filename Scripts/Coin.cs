using Godot;
using System;

public class Coin : PickableItem
{

    public void _on_Area_body_entered(Node2D body)
    {
        root.money++;
        DestroyThisObj();
    }
    
}
