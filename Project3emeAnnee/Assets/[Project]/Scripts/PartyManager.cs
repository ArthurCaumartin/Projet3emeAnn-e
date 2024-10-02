using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public enum PartyState
{
    None,
    Mobile,
    TowerDefence,
    TowerDefencePlacement,
}

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance;
    [SerializeField] private PartyState _state;
    [Space]
    [SerializeField] private PlayerControler _playerControler;
    [SerializeField] private CameraControler _camControler;
    [SerializeField] private TurretManager _turretManager;

    public PartyState GameState { get => _state; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetPartyState(PartyState.Mobile);
    }

    public void StartTowerDefencePlacement(Transform siphonTransform)
    {
        SetPartyState(PartyState.TowerDefencePlacement);
        _playerControler.SetControlerInSiphonMode(siphonTransform);
    }

    public void StartTowerDefence()
    {
        //TODO call spawn
        // _turretManager
    }

    public void SetPartyState(PartyState toSet)
    {
        if (toSet == _state) return;
        _state = toSet;

        switch (toSet)
        {
            case PartyState.Mobile:
                _playerControler.SetControlerInMobileMode();
                break;

            case PartyState.TowerDefence:
                //!
                break;
        }

        _camControler.SetCameraOnState(_state);
    }
}
