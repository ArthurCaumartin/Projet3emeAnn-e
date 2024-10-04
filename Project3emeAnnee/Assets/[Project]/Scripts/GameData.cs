using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    private int _globalGas = 0;
    public int GlobalGas{ get => _globalGas; }

    private int _difficulty = 1;
    public int Difficulty { get => _difficulty; }
    private void Awake()
    {
        instance = this;
        
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeDifficulty(bool addDiff)
    {
        if (addDiff)
        {
            _difficulty++;
        }
        else
        {
            _difficulty = 1;
        }
    }

    public bool UseGas(int valueToChange)
    {
        if (valueToChange > 0)
        {
            valueToChange = -valueToChange;
        }
        
        if (_globalGas + valueToChange >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }

    public void AddGas(int valueToChange)
    {
        _globalGas += valueToChange;
        if (_globalGas > 9999) _globalGas = 9999;
    }
}
    