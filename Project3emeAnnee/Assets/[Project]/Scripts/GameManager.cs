using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum GameState
{
    None,
    Mobile,
    TowerDefence,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameState _state;
    [Space]
    [SerializeField] private CameraControler _camControler;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetGameState(GameState.Mobile);
    }

    public void SetGameState(GameState toSet)
    {
        if (toSet == _state) return;
        _state = toSet;
        _camControler.SetCameraOnState(_state);
    }
}
