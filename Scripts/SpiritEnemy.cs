using Godot;
using System;

public class SpiritEnemy : SpiritCreature
{

    protected float rPlayerDist = 1200; // dist to player for rebirth
    
    protected bool rebirthAble = true;

    protected bool CanRebirth() // ?? better dist func ??
    {
        if (root.spiritPlayer == null)
        {
            return true;
        }
        float nowDist = Functions.vSqLen(root.spiritPlayer.Position - this.Position);
        float rebirthDist = Functions.vSqLen(root.spiritPlayer.Position - lastRebirthPos);
        if (Mathf.Min(nowDist, rebirthDist) > rPlayerDist * rPlayerDist)
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
        ItemsSpawner.GenItem(this,  0, this.Position +  new Vector2(0, -50), Vector2.Zero, "Item_2_0n", 0, 0);
    }

    public override void _Ready()
    {
        base._Ready();
        maxSoul = 5000;
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
