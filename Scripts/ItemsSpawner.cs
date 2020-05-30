using Godot;
using System;

public static class ItemsSpawner
{

    private static PackedScene coin, crystal, spellObj;
    private static bool resLoaded = false;

    public static void LoadRes()
    {
        coin = Functions.LoadPScene("coin");
        crystal = Functions.LoadPScene("crystal");
        spellObj = Functions.LoadPScene("spell_obj");
        resLoaded = true;
    }

    public static void GenItem(Node2D creator, int type, Vector2 pos, Vector2 direction,  string name = "Item", float minV = 0, float maxV = 0)
    {
        if (!resLoaded)
        {
            LoadRes();
        }
        Vector2 speed;
        if (direction == Vector2.Zero)
        {
            if (maxV == 0)
            {
                speed = Vector2.Zero;
            }
            else
            {
                speed = Functions.NormV(new Vector2((float)(Constants.rand.NextDouble() - 0.5d), (float)(Constants.rand.NextDouble() - 0.5d)));
            }
        }
        else
        {
            speed = Functions.NormV(direction);
        }
        speed *= minV + (float)(Constants.rand.NextDouble() * (minV - maxV));
        if (type >= 0 && type < Constants.ITEM_NUM)
        {
            PhysicsObj obj = null;
            switch(type)
            {
                case 0:
                    obj = (PhysicsObj)coin.Instance(); 
                    break;
                case 1:
                    obj =  (PhysicsObj)crystal.Instance();
                    break;
                case 2:
                    obj = (PhysicsObj)spellObj.Instance();
                    break;
                default:
                    break;
            }
            if (obj != null)
            {
                obj.Position = pos;
                obj.move = speed;
                obj.Name = name;
                creator.GetTree().Root.AddChild(obj);
            }
        }
    }

}
