using Godot;
using System;

public class SFlyingE : SpiritEnemy
{
    
    protected float ATTACK_DIST_TO_P = 300; // attack dist to player

    protected float MAX_DIST_TO_REBIRTH = 600;
    protected bool lastPNear = false;

    protected override void UseAI()
    {
        if (root.activePlayer != null && Functions.vSqLen(root.activePlayer.Position - this.Position) < ATTACK_DIST_TO_P * ATTACK_DIST_TO_P)
        {
            CreaturesAI.FlyToPos(this, root.activePlayer.Position);
            lastPNear = true;
        }
        else
        {
            if (Functions.vSqLen(lastRebirthPos - this.Position) < MAX_DIST_TO_REBIRTH * MAX_DIST_TO_REBIRTH) // ??
            {
                if (lastPNear)
                {
                    move = Vector2.Zero;
                }
                CreaturesAI.RandomFly(this);
                lastPNear = false;
            }
            else
            {
                CreaturesAI.FlyToPos(this, lastRebirthPos);
            }
        }
    }

    public override void _Ready()
    {
        base._Ready();
        gravityActive = false;
        speed = new Vector2(200, 200);
    } 

}
