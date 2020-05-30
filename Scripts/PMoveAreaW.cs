using Godot;
using System;

public class PMoveAreaW : Area2D // player move area weapon
{

    protected AnimatedSprite sprite;
    protected Vector2 move;
    protected float existTime;

    public override void _Ready()
    {

    }

    public void Init(Vector2 _move, float _existTime) // add timer
    {
        move = _move;
        existTime = _existTime;
        sprite = (AnimatedSprite)GetNodeOrNull("ASprite");
        if (sprite != null)
        {
            sprite.FlipH = (move.x < 0);
        }
    }

    public virtual void DamageCreature(Creature c)
    {

    }

    public virtual void DamageDBlock(DestroyableBlock b)
    {

    }

    public virtual void ActionOnTouch(Node2D body)
    {
        try
        {
            Creature c = (Creature)body;
            if (!c.GetDied())
            {
                DamageCreature(c);
                QueueFree();
            }
        }
        catch
        {
            try
            {
                DestroyableBlock b = (DestroyableBlock)body;
                DamageDBlock(b);
            }
            catch {}
        }
        QueueFree();
    }

    public void _on_PWMoveArea_body_entered(Node2D body)
    {
        ActionOnTouch(body);
    }

    public override void _Process(float delta)
    {
        MoveLocalX(move.x, true);
        MoveLocalY(move.y, true);
        existTime -= delta;
        if (existTime <= 0)
        {
            QueueFree();
        }
    }

}
