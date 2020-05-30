using Godot;
using System;

public class MPMenu : Control
{

    private Root root;
    private Label money,changeForm, soul;
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
            GD.Print("Magic player menu items connect error.");
        }
    }

    public override void _Process(float delta)
    {
        money.Text = "Money: " + root.money;
        changeForm.Text = "Change player form num: " + root.changePForm;
        if (root.magicPlayer != null)
        {
            soul.Text = "Soul: " + root.magicPlayer.GetSoul();
        }
    }
}
