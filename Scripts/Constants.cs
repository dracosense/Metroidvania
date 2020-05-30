using Godot;
using System;

public static class Constants
{

    public static Random rand = new Random();

    public static readonly Color  MAGIC_C = Colors.White, SPIRIT_C = Colors.Black, MIND_C = Colors.Purple; 
    public const float START_Y_MOVE = 10;
    public const float GRAVITY = 630.0f;
    public const int CRYSTAL_TYPES_NUM = 3; // knowledge / nature / rage 
    public const int CRYSTAL_FORMS_NUM = 3; // + / 0 / -
    public const int M_SPELL_STACK_SIZE = 3;
    // player  
    public const float MAGIC_SPHERE_SPEED = 4.0f;
    public const float SPIRIT_ARROW_SPEED = 6.0f;
    public const int ATTACK_KEY_NUM = 9;
    public const int MAGIC_SPELL_LIST_SIZE = 5;
    public const int ITEM_NUM = 3;
    public const bool CAN_ATTACK_WHEN_MOVE = true;
    
}
