using System;
using UnityEngine;

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
    [SerializeField] private PartyCanvas _partyCanvas;
    [SerializeField] private TurretManager _turretManager;
    [SerializeField] private SpawnMobileMob _spawnMobileMob;
    [Space]
    [SerializeField] private float _totalGazCollect = 0;

    public PartyState GameState { get => _state; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetPartyState(PartyState.Mobile);
        _partyCanvas.UpdateLifeBar(10, 10);
        _partyCanvas.UpdateGazCount(_totalGazCollect);
    }

    public void StartTowerDefencePlacement(Transform siphonTransform)
    {
        SetPartyState(PartyState.TowerDefencePlacement);
        _playerControler.SetControlerInSiphonMode(siphonTransform);
    }

    public void StartTowerDefence()
    {
        SetPartyState(PartyState.TowerDefence);
    }

    public void SetPartyState(PartyState toSet)
    {
        if (toSet == _state) return;
        _state = toSet;
        // print(toSet);
        switch (toSet)
        {
            case PartyState.Mobile:
                _playerControler.SetControlerInMobileMode();
                break;

            case PartyState.TowerDefencePlacement:
                _spawnMobileMob.Nuke();
                break;

            case PartyState.TowerDefence:
                //!
                break;
        }

        _partyCanvas.UpdatePartyState(_state);
        _turretManager.PassPartyState(_state);
        _camControler.SetCameraOnState(_state);
    }

    public void PlayerTakeDamage(int maxLife, int currentLife)
    {
        _partyCanvas.UpdateLifeBar(maxLife, currentLife);
        if (currentLife <= 0)
        {
            print("Player is Dead !");
        }
    }

    public void CollectGaz(float value)
    {
        _totalGazCollect += value;
        _partyCanvas.UpdateGazCount(_totalGazCollect);
        GameManager.instance.AddGas((int)_totalGazCollect);
    }
}
