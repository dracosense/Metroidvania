using Godot;
using System;

public class PhysicsObj : KinematicBody2D
{

    protected Root root;
    protected Vector2 gravity = new Vector2(0, 1);
    protected Vector2 lastPosition;
    protected Vector2 lastOnFloorPos;
    protected int mapNum; // pos on map
    protected bool lastMoveLeft = true;
    protected bool gravityActive = true;
    protected bool simpleFormColor = true;
    public Vector2 move;
    public int formMask = 7; // magic, spirit, mind
    public int saveOnFloorPos = 0; // if 0 - save, else - not save

    public void PhysicsMove(float delta, bool setFloorPos  = true) // make last n floor pos getter better
    {
        if (move.x != 0)
        {
            lastMoveLeft = (move.x > 0);
        }
        if (gravityActive)
        {
            //if (!IsOnFloor())
            //{
            move.y += Constants.GRAVITY * delta;
            //}
            if (IsOnCeiling())
            {
                move.y = Mathf.Max(move.y, 0);
            }
            if (IsOnFloor())
            {
                move.y = Mathf.Min(move.y, Constants.START_Y_MOVE);
                if (setFloorPos && saveOnFloorPos == 0)
                {
                    lastOnFloorPos = lastPosition;
                }
                lastPosition = this.Position;
            }
        }
        MoveAndSlide(new Vector2(move.x * gravity.y + move.y * gravity.x, move.y * gravity.y - move.x * gravity.x), -gravity);
    }

    public void SetGravityActive(bool a)
    {
        gravityActive = a;
    }

    public bool GetGravityActive()
    {
        return gravityActive;
    }

    public void MoveToLastOnFloor()
    {
        this.Position = lastOnFloorPos;
        move = Vector2.Zero;
        //
        mapNum = 0;
        //
    }

    public Vector2 GetGravity()
    {
        return gravity;
    }

    public int GetMapNum()
    {
        return mapNum;
    }

    public void SetMapNum(int a)
    {
        mapNum = a;
    }

    public override void _Ready()
    {
        move = new Vector2(0, 0);
        lastPosition = this.Position;
        lastOnFloorPos = this.Position;
        root = (Root)GetNode("/root/root/");
        this.RotationDegrees = Mathf.Atan2(gravity.y, gravity.x) * (180.0f / Mathf.Pi) -  90.0f;
    }

    public virtual void PhysicsProcess(float delta)
    {
        PhysicsMove(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        if ((formMask & (1 << root.GetActiveForm())) != 0)
        {
            if (simpleFormColor)
            {
                switch (root.GetActiveForm())
                {
                    case 0:
                        Modulate = Constants.MAGIC_C;
                        break;
                    case 1:
                        Modulate = Constants.SPIRIT_C;
                        break;
                    case 2:
                        Modulate = Constants.MIND_C;
                        break;
                    default:
                        Modulate = Colors.White;
                        break;
                }
            }
            Visible = true;
            PhysicsProcess(delta);
        }
        else
        {
            Visible = false;
        }
    }
}
