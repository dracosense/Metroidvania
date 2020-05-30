using Godot;
using System;

public class MagicEnemy : MagicCreature
{

    protected float rPlayerDist = 1200; // dist to player for rebirth
    
    protected bool rebirthAble = true;

    protected bool CanRebirth() // ?? better dist func ??
    {
        if (root.magicPlayer == null)
        {
            return true;
        }
        float nowDist  = Functions.vSqLen(root.magicPlayer.Position - this.Position);
        float rebirthDist = Functions.vSqLen(root.magicPlayer.Position - lastRebirthPos);
        if (Mathf.Min(nowDist, rebirthDist) >  rPlayerDist * rPlayerDist)
        {
            return true;
        }
        return false;
    }

    protected virtual void UseAI()
    {
        CreaturesAI.Simple(this);
    }

    protected override void OnDestroyEvent()
    {
        ItemsSpawner.GenItem(this,  0, this.Position, Vector2.Zero, "Item_1_0n", 0, 0);
    }

    public override void _Ready()
    {
        base._Ready();
        param.maxSoul = 5000;
        lastRebirthPos = this.Position;
        speed.x = 100.0f;
    }

    public override void PhysicsProcess(float delta)
    {
        if (!stunned)
        {
            UseAI();
        }
        else
        {
            move.x = 0;
        }
        base.PhysicsProcess(delta);
        if (died && rebirthAble && CanRebirth())
        {
            Rebirth();
        }
    }

}
