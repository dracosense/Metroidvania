using Godot;
using System;

public static class PMWeaponA // player magic weapon attacks // add spells to spell list
{

    private static PackedScene sphereR, handR;
    private static bool resLoaded = false;

    public static void LoadRes()
    {
        sphereR = Functions.LoadPScene("PMAttacks/p_sphere_w");
        handR = Functions.LoadPScene("PMAttacks/p_hand_w");
        resLoaded = true;        
    }

    private static Node getMMapNode(Node2D c)
    {
        return c.GetTree().Root.GetNode("Game/Magic/MagicMap");
    }

    public static Node SphereA(bool lastMoveLeft, Node2D c)
    {
        if (!resLoaded)
        {
            LoadRes();
        }
        try
        {
           DamageSAreaW a = (DamageSAreaW)sphereR.Instance();
            a.RotationDegrees = c.RotationDegrees;
            Functions.InitAPosObj(lastMoveLeft, a, c);
            a.Init(new Vector2(Constants.MAGIC_SPHERE_SPEED * (lastMoveLeft?1:-1), 0), 10);
            a.DamageInit(4000, 0.1f, 0, c.Position);
            getMMapNode(c).AddChild(a);
            return a;
        } 
        catch 
        {
            return null;
        }
    }

    public static Node HandA(bool lastMoveLeft, Node2D c)
    {
        if (!resLoaded)
        {
            LoadRes();
        }
        try
        {
            DamageSAreaW a = (DamageSAreaW)handR.Instance();
            Functions.InitAPosObj(lastMoveLeft, a, c);
            a.RotationDegrees = c.RotationDegrees;
            a.Init(Vector2.Zero, 0.1f);
            a.DamageInit(1000, 0.2f, 2000, c.Position);
            getMMapNode(c).AddChild(a);
            return a;
        } 
        catch 
        {
            return null;
        }
    }

    public static void DamageCreature(MagicCreature c, int num, int damage)
    {
        switch(num)
        {
            default:
                c.DamageSoul(damage);
                break;
        }
    }
}
