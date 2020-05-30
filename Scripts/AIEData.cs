using Godot;
using System;

public class AIEData
{

    private float time;
    private int state;
    public bool lastMove;

    public AIEData()
    {
        state = 0;
        time = 0;
    }

    public int GetState()
    {
        return state;
    }

    public float GetTime()
    {
        return time;
    }

    public void  SetState(int _state)
    {
        state = _state;
        time = 0;
    }

    public void Update(float delta)
    {
        time += delta;
    }
}
