using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasolineManager : MonoBehaviour
{
    public int _gasoline;

    public bool BuyCore(int value)
    {
        if (value <= _gasoline)
        {
            _gasoline -= value;
            return true;
        }
        else
        {
            return false;
        }
    }
}
