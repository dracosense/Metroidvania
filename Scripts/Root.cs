using Godot;
using System;

public class Root : Node
{
    public MagicPlayer magicPlayer;
    public SpiritPlayer spiritPlayer;
    // public MindPlayer mindPlayer
    public ACreature activePlayer;

    // crystalls
    public const int C_FORMS_NUM = 3; // live, rage, knowledge
    public const int C_TYPES_NUM = 3; // + 0 -
    public const int CHANGE_PLAYER_FORM_MAX = 300; 
    public int[, ] crystalls;
    public int money = 0;
    public int changePForm = CHANGE_PLAYER_FORM_MAX;
    public int pSaveCNum = 0;

    private Node2D magicEnv;
    private Node2D spiritEnv;
    private Node2D mindEnv;

    private Vector2 playerPos;
    private Vector2 playerMove;
    private int playerMapNum;
    private bool firstPUpdate = true;

    private int activePNum = 0;
    private int lastActivePNum = -1;

    private void updateAPlayer()
    {
        if (lastActivePNum != activePNum)
        {
            switch(activePNum)
            {
                case 0:
                    activePlayer = magicPlayer; 
                    break;
                case 1:
                    activePlayer = spiritPlayer;
                    break;
                case 2:
                    //activePlayer = mindPlayer;
                    break;
                default:
                    break;
            }
            magicEnv.Visible = (activePNum == 0);
            spiritEnv.Visible = (activePNum == 1);
            mindEnv.Visible = (activePNum == 2);
            if (magicPlayer != null)
            {
                magicPlayer.Visible = (activePNum == 0);
            }
            if (spiritPlayer != null)
            {
                spiritPlayer.Visible = (activePNum == 1);
            }
            // if (mindPlayer != null)
            // {
                // mindPlayer.Visible = (activePNum == 2);
            // }
            lastActivePNum = activePNum;
            if (!firstPUpdate)
            {
                activePlayer.SetPos(playerPos);
                activePlayer.move = playerMove;
            }
            activePlayer.SetMapNum(playerMapNum);
            firstPUpdate = false;
        }
    }

     private void PlayerRebirth()
    {
        magicPlayer.Rebirth();
        spiritPlayer.Rebirth();
        // mindPlayer.Rebirth();
    }

    public void PlayerRegenerate()
    {
        magicPlayer.Regenerate();
        spiritPlayer.Regenerate();
        // mindPlayer.Regenerate();
    }

    public void SetRebirthPoint(Vector2 v)
    {
        magicPlayer.SetRebirthPoint(v);
        spiritPlayer.SetRebirthPoint(v);
        // mindPlayer.SetRebirthPoint(v);
    }

    public int GetActiveForm()
    {
        return activePNum;
    }

    public void AddCrystal(int c)
    {
        crystalls[c / C_FORMS_NUM, c % C_TYPES_NUM]++;
    }

    public override void _Ready()
    {
        crystalls = new int[C_FORMS_NUM, C_TYPES_NUM];
        magicEnv = (Node2D)GetNode("/root/Game/Magic");
        spiritEnv = (Node2D)GetNode("/root/Game/Spirit");
        mindEnv = (Node2D)GetNode("/root/Game/Mind");
        ItemsSpawner.LoadRes();
    }

    public override void _Process(float delta)
    {
        if (activePlayer != null)
        {
            if (activePlayer.GetDied())
            {
                PlayerRebirth();
            }
            playerPos = activePlayer.Position;
            playerMove = activePlayer.move;
        }
        if (Input.IsActionJustPressed("change_player") && changePForm > 0 && pSaveCNum > 0)
        {
            activePNum = 1 - activePNum;
            changePForm--;
        }
        updateAPlayer();
    }
}
