using Godot;
using System;

public class SPMenu : Control
{
    private Root root;
    private Label money, changeForm, soul;

    public override void _Ready()
    {
        try
        {
            root = (Root)GetNode("/root/root");
            money = (Label)GetNode("Money");
            changeForm = (Label)GetNode("ChangeForm");
            soul = (Label)GetNode("Soul");
        }
        catch
        {
            GD.Print("Spirit player items connect error.");
        }
    }

    public override void _Process(float delta)
    {
        money.Text = "Money: " + root.money;
        changeForm.Text = "Change player form num: " + root.changePForm;
        if (root.spiritPlayer != null)
        {
            soul.Text = "Soul: " + root.spiritPlayer.GetSoul();
        }
    }
}
