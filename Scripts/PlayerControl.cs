using Godot;
using System;

public class PlayerControl
{
    public static Vector2 MoveControl(ACreature creature)
	{
        Vector2 move = creature.move;
        float x = Mathf.Min(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), 1.0f);
        float y = -Mathf.Min(Input.GetActionStrength("ui_jump"), 1.0f);
        if (x != 0 || y != 0)
        {
            creature.SetAttacking(false);
        }
        move.x = creature.GetSpeed().x * x;
        if (y != 0 && creature.IsOnFloor())
        {
            move.y = creature.GetSpeed().y * y;
        }
        return move;
	}

    public static int GetAttackNum()
    {
		for (int i = 0; i < Constants.ATTACK_KEY_NUM; i++)
		{
			if (Input.IsActionJustPressed((i + 1).ToString()))
			{
				return i;
			}
		}
        return -1;
    }
}
