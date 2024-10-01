using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public enum GameState
{
    None,
    Mobile,
    TowerDefence,
}

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance;
    [SerializeField] private GameState _state;
    [Space]
    [SerializeField] private PlayerControler _playerControler;
    [SerializeField] private CameraControler _camControler;
    [SerializeField] private TurretManager _turretManager;

    public GameState GameState { get => _state; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetGameState(GameState.Mobile);
    }

    public void StartSihpon(Transform siphonTransform)
    {
        _state = GameState.TowerDefence;
        _camControler.SetCameraOnState(_state);
        _playerControler.SetControlerInSiphonMode(siphonTransform);
    }

    public void SetGameState(GameState toSet)
    {
        if (toSet == _state) return;
        _state = toSet;

        switch (toSet)
        {
            case GameState.Mobile:
                //!
                break;

            case GameState.TowerDefence:
                //!
                break;
        }

        _camControler.SetCameraOnState(_state);
    }
}
