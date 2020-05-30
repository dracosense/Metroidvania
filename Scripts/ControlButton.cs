using Godot;
using System;
using System.Collections.Generic;

public class ControlButton : Button
{

    List<string> actions;

    public override void _Ready()
    {
        string s = "";
        actions = new List<string>();
        for (int i = 0; i < this.Name.Length; i++)
        {
            if (this.Name[i] == '&')
            {
                actions.Add(s);
                s = "";
            }
            else
            {
                s = s + this.Name[i];
            }
        }
        actions.Add(s);
    }

    public void _on_ControlButton_down()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            Input.ActionPress(actions[i]);
        }  
    }

    public void _on_ControlButton_up()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            Input.ActionRelease(actions[i]);
        }
    }
}
