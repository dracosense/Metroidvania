using Godot;
using System;
using System.Collections.Generic;

public class Functions
{

    public class MPStruct
    {
        public int live, rage, magic;
        public MPStruct()
        {
            live = 0;
            rage = 0;
            magic = 0;
        }

        public MPStruct(int _live, int _rage, int _magic)
        {
            live = _live;
            rage = _rage;
            magic = _magic;
        }

        /*public int GetLive()
        {
            return live;
        }
        
        public int GetRage()
        {
            return rage;
        }

        public int GetMagic()
        {
            return magic;
        }*/

        public int GetParam(int i)
        {
            switch(i)
            {
                case 0:
                    return live;
                case 1:
                    return rage;
                case 2:
                    return magic;
                default:
                    return 0; // ??
            }
        }

        /*public void SetLive(int _live)
        {
            live = _live;
        }

        public void SetRage(int _rage)
        {
            rage = _rage;
        }

        public void SetMagic(int _magic)
        {
            magic = _magic;
        }*/

        public void SetParam(int i, int param)
        {
            if (i >= 0 && i < Constants.CRYSTAL_TYPES_NUM)
            {
                switch(i)
                {
                    case 0:
                        live = param;
                        break;
                    case 1:
                        rage = param;
                        break;
                    default:
                        magic = param;
                        break;
                }
            }
        }

    } 

    public static List<int> GetParamsFromName(string name) // ?? better way ?? 
    {
        int a = 0, x = 0;
        List<int> p = new List<int>();
        for (int i = 0; i < name.Length; i++)
        {
            if (a == 1)
            {
                if (name[i] >= '0' && name[i] <= '9')
                {
                    x *= 10;
                    x += name[i] - '0';
                }
                else
                {
                    p.Add(x);
                    x = 0;
                    a = 0;
                }
            }
            if (name[i] == '_')
            {
                if (a == 1)
                {
                    p.Add(x);
                    x = 0;
                }
                a = 1;
            }
        }
        if (a == 1)
        {
            p.Add(x);
        }
        return p;
    }

    public static Vector2 InitAPos(bool lastMoveLeft, Vector2 pos, Vector2 playerPos, Vector2 rotation)
    {
        if (!lastMoveLeft)
        {
            pos.x = -pos.x;
        }
        Vector2 v = new Vector2(pos.x * rotation.y - pos.y * rotation.x + playerPos.x, pos.x * rotation.x + pos.y * rotation.y + playerPos.y);
        return v;
    }

    public static void InitAPosObj(bool lastMoveLeft, Node2D a, Node2D c)
    {
        a.Position = InitAPos(lastMoveLeft, a.Position, c.Position, new Vector2(Mathf.Sin(c.Rotation), Mathf.Cos(c.Rotation)));
    }

    public static PackedScene LoadPScene(string localPath)
    {
        try
        {
            return (PackedScene)ResourceLoader.Load("res://Scenes/" + localPath + ".tscn");
        }
        catch
        {
            GD.Print("Load scene res error: " + localPath);
            return null;
        }
    }

    public static float Mod(float x)
    {
        return Mathf.Max(x, -x);
    }

    public static float vSqLen(Vector2 v)
    {
        return v.x * v.x + v.y * v.y;
    }

    public static float vLen(Vector2 v)
    {
        return Mathf.Sqrt(vSqLen(v));
    }

    public static Vector2 NormV(Vector2 v)
    {
        float len = vLen(v);
        if (len != 0)
        {
            v /= len;
        } 
        return v;
    }
}
