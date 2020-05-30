using Godot;
using System;

public class DestroyableBlock : StaticBody2D
{

    protected int strength = 1000;
    
    public virtual void Damage(int damage)
    {
        strength -= damage;
        if (strength <= 0)
        {
            QueueFree();
        }
    }
}
