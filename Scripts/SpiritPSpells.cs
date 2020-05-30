using Godot;
using System;

public static class SpiritPSpells // add spell to spell list
{

    private static PackedScene platformR, swordR, arrowR;
    private static bool resLoaded = false;
    public static void LoadRes()
    {
        platformR = Functions.LoadPScene("PSAttacks/p_s_platform_w");
        swordR = Functions.LoadPScene("PSAttacks/p_s_sword_w");
        arrowR = Functions.LoadPScene("PSAttacks/p_s_arrow_w");
        resLoaded = true;
    }

    public static Node GetSMapNode(Node2D c)
    {
        return c.GetTree().Root.GetNode("Game/Spirit/SpiritMap");
    }
    public static float GetCastTime(int n)
    {
        switch(n)
        {
            case 0:
                return 0;
            case 1:
                return 0.375f; 
            case 2:
                return 0.8f;
            default:
                return -1;
        }   
    }

    public static Node PlatformS(Node2D c)
    {
        if (!resLoaded)
        {
            LoadRes();
        }
        try
        {
                PSPlatformW a = (PSPlatformW)platformR.Instance();
                a.Position = c.Position;
                a.Rotation = c.Rotation;
                GetSMapNode(c).AddChild(a);
                return a;
            }
        catch 
        {
            return null;
        }
    }

    public static Node SwordA(bool lastMoveLeft, Node2D c)
    {
        if (!resLoaded)
        {
            LoadRes();
        }
        try
        {
            DamageSAreaW a = (DamageSAreaW)swordR.Instance();
            Functions.InitAPosObj(lastMoveLeft, a, c);
            a.Init(Vector2.Zero, 0.5f);
            a.DamageInit(1000, 0.2f, 2000, c.Position);
            a.Rotation = c.Rotation;
            GetSMapNode(c).AddChild(a);
            return a;
        }
        catch 
        {
            return null;
        }
    }

    public static Node ArrowA(bool lastMoveLeft, Node2D c)
    {
        if (!resLoaded)
        {
            LoadRes();
        }
        try
        {
            DamageSAreaW a = (DamageSAreaW)arrowR.Instance();
            Functions.InitAPosObj(lastMoveLeft, a, c);
            a.Init(new Vector2(Constants.SPIRIT_ARROW_SPEED * (lastMoveLeft?1:-1), 0), 8.0f);
            a.DamageInit(800, 0.1f, 0, c.Position);
            a.Rotation = c.Rotation;
            GetSMapNode(c).AddChild(a);
            return a;
        }
        catch
        {
            return null;
        }
    }

    public static string GetAttackAName(int n)
    {
        switch(n)
        {
            case 0:
                return "attack1";
            case 1:
                return "attack1";
            case 2:
                return "attack2";
            default:
                return "";
        }
    }
}
