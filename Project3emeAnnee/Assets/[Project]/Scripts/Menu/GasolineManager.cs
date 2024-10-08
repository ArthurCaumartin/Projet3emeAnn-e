using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasolineManager : MonoBehaviour
{
    [SerializeField] private int _gasoline;
    public int Gasoline { get => _gasoline; set => _gasoline = value; }

    private void Start()
    {
        _gasoline = GameManager.instance.GlobalGas;
    }
}