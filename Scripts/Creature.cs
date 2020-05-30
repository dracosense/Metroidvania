using Godot;
using System;

public class Creature : PhysicsObj
{

    protected Vector2 speed = new Vector2(200.0f, 540.0f); //180, 420
    protected AnimatedSprite sprite;
    protected Vector2 lastRebirthPos;
    protected float outGameTime = 0.0f; // time without damage
    protected float outGameT; // timer
    protected float stunnedT; // stunned timer
    protected bool outOfGame;
    protected bool stunned = false;
    protected bool died = false;

    protected void SetAnimation()
    {
        if (sprite != null && !died)
        {
            if (move.x != 0)
            {
                sprite.FlipH = !lastMoveLeft;
            }
            if ((move.x != 0) && IsOnFloor())
            {
                sprite.Animation = "move";
            }
            else
            {
                if (IsOnFloor())
                {
                    sprite.Animation = "idle";
                }
                else
                {
                    if (move.y < 0)
                    {
                        sprite.Animation = "jump";
                    }
                    else
                    {
                        sprite.Animation = "fall";
                    }
                }
            }
        }
    }

    protected void SetOutGameSh()
    {
        try
        {
            ((ShaderMaterial)sprite.Material).SetShaderParam("out_of_game", outOfGame);
        }
        catch {}
    }

    public Root GetRoot()
    {
        return root;
    }

    public bool GetDied()
    {
        return died;
    }

    public Vector2 GetSpeed()
    {
        return speed;
    }

    public void SetPos(Vector2 pos)
    {
        this.Position = pos;
        move = Vector2.Zero;
    }

    public virtual void Regenerate()
    {
        died = false;
        move = Vector2.Zero;
        outGameT = 0;
        stunnedT = 0;
        outOfGame = false;
        stunned = false;
    }

    public void Rebirth()
    {
        this.Position = lastRebirthPos;
        Regenerate();
    }

    public void SetRebirthPoint(Vector2 v)
    {
        lastRebirthPos = v;
    }

    public void SetStunned(float time)
    {
        if (time == 0)
        {
            return;
        }
        stunned = true;
        stunnedT = Mathf.Max(stunnedT, time);
    }

    public virtual void UpdateGameTimers(float delta)
    {
        // out game timer
        SetOutGameSh();
        if (outOfGame)
        {
            if (outGameT <= delta)
            {
                outGameT = 0;
                outOfGame = false;
            }
            else
            {
                outGameT -= delta;
            }
        }
        // stunned timer
        if (stunnedT <= delta)
        {
                stunnedT = 0;
                stunned = false;
        }
        else
        {
            stunnedT -= delta;
        }
    }

    public virtual void SacrificeSoul(int damage)
    {
        GD.Print("Wrong target for damage.");
    }

    public virtual void DamageSoul(int damage)
    {
        if (damage > 0 && !outOfGame)
        {
            outGameT = outGameTime;
            outOfGame = true;
        }
        //
    }

    public override void _Ready()
    {
        base._Ready();
        sprite = (AnimatedSprite)GetNode("ASprite");
        lastRebirthPos = this.Position;
        simpleFormColor = false;
        Regenerate();
    }

    public override void PhysicsProcess(float delta)
    {
        UpdateGameTimers(delta);
        PhysicsMove(delta);
        SetAnimation();
    }
}
