using Godot;
using System;

public class CreaturesAI
{

    public static void Simple(Creature c)
    {
        if (c.GetDied())
        {
            c.move.x = 0;
            return;
        }
        if (c.IsOnWall())
        {
            c.move.x = -c.move.x;
        }
        else
        {
            if (c.move.x == 0.0f)
            {
                c.move.x = ((Constants.rand.Next() % 2 == 0)?-1:1) * c.GetSpeed().x;
            }
        }
    }

    public static void FlyToPos(Creature c, Vector2 pos)
    {
        if (c.GetDied())
        {
            c.move = Vector2.Zero;
            c.SetGravityActive(true);
            return;
        }
        Vector2 s = c.GetSpeed();
        if (c.Position != pos)
        {
            Vector2 d = Functions.NormV(pos - c.Position);
            c.move = new Vector2(s.x * d.x, s.y * d.y);
        }
        else
        {
            c.move.x = 0;
        }
    }

    public static void MoveToPos(Creature c, Vector2 pos)
    {
        if (c.GetDied())
        {
            c.move = Vector2.Zero;
            return;
        }
        if (c.Position != pos)
        {
            float x = (pos - c.Position).x;
            c.move.x = c.GetSpeed().x * x / Functions.Mod(x);
            if (c.IsOnWall() && c.IsOnFloor())
            {
                c.move.y = -c.GetSpeed().y;
            }
        }
        else
        {
            c.move.x = 0;
        }
    }

    public static void RandomFly(Creature c)
    {
        if (c.GetDied())
        {
            c.move = Vector2.Zero;
            return;
        }
        if (c.move == Vector2.Zero)
        {
                float x = (float)(2 * Mathf.Pi * (Constants.rand.NextDouble()));
                c.move = new Vector2(c.GetSpeed().x * Mathf.Cos(x), c.GetSpeed().y * Mathf.Sin(x));
        }
        if (c.IsOnWall())
        {
            c.move.x = -c.move.x;
        }
        if (c.IsOnFloor() || c.IsOnCeiling())
        {
            c.move.y = -c.move.y;
        }
    }

}
