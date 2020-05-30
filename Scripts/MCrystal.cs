using System;

public class MCrystal
{
    protected bool init = false;
    protected int[] num;
    protected int[] form; 
    public int Set(int[] _num,  int[] _form)
    {
        try
        {
            init = true;
            num = new int[Constants.CRYSTAL_TYPES_NUM];
            form = new int[Constants.CRYSTAL_TYPES_NUM];
            for (int i = 0; i < Constants.CRYSTAL_TYPES_NUM; i++)
            {
                num[i] = _num[i];
                form[i] = _form[i];
            }
            return 0;
        }
        catch
        {
            init = false;
            return -1;
        }
    }

    public int GetNum(int n)
    {
        if (n < 0 || n >= Constants.CRYSTAL_TYPES_NUM || !init)
        {
            return -1;
        }
        return num[n];
    }

    public int GetForm(int n)
    {
        if (n < 0 || n >= Constants.CRYSTAL_TYPES_NUM || !init)
        {
            return -1;
        }
        return form[n];
    }

    public int GetMax()
    {
        int mx = -1, mi = -1;
        if (!init)
        {
            return -1;
        }
        for (int i = 0; i < num.Length; i++)
        {
            if (num[i] > mx)
            {
                mx = num[i];
                mi = i;
            }
        }
        return mi;
    }
}
