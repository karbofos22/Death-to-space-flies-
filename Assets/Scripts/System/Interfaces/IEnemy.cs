using System.Collections;
using System.Collections.Generic;

public interface IEnemy
{
    private int Hp
    {
        get
        {
            return Hp;
        }
        set
        {
            Hp = value;
        }
    }

    int ScoreValue
    {
        get
        {
            return ScoreValue;
        }
        set
        {
            ScoreValue = value;
        }
    }
}
