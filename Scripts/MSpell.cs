using System;

public class MSpell
{

    protected MCrystal crystal;
    protected int lastType;
    protected int[] types;

    public MSpell()
    {
        lastType = 0;
        types = new int[Constants.M_SPELL_STACK_SIZE];
        for (int i = 0; i < types.Length; i++)
        {
            types[i] = -1;
        }
    }

    public void SetT(int _type)
    {
        lastType++;
        lastType %= Constants.M_SPELL_STACK_SIZE;
        types[lastType] = _type;
    }

    public int SetCrystalM(int[] num, int[] form)
    {
        return crystal.Set(num, form);
    }

    public void SetCrystal(MCrystal _crystal)
    {
        crystal = _crystal;
    }

    public float GetCastTime()
    {
        return 0.3f;    
    }

    public string GetAttackAName()  // animation
    {
        return "attack" + 0.ToString();
    }

    public void DeleteCrystal() // ??
    {
        crystal = null;
    }

}
