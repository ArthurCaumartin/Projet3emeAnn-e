using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [Header("Turret Panel : ")]
    [SerializeField] private GameObject _turretPanelPrefab;
    [SerializeField] private RectTransform _turretPanelContainer;
    [Space]
    [Header("Turret : ")]
    [SerializeField] private GameObject _turretPrefab;
    private List<GameObject> _turretPanelList = new List<GameObject>();
    private List<GameObject> _turretList = new List<GameObject>();
    private TurretPanel _lastPanelOpen;
    private GameObject _gostTurret;
    private Grid _grid;
    private Camera _mainCam;
    private List<TurretBaker> _turretPlacedList = new List<TurretBaker>();

    void Start()
    {
        _mainCam = Camera.main;
        _grid = GetComponentInChildren<Grid>();
        InstantiatePanelAndTurret(4);
    }

    public void InstantiatePanelAndTurret(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newTurret = Instantiate(_turretPrefab, transform);
            _turretList.Add(newTurret);
            newTurret.SetActive(false);

            GameObject newPanel = Instantiate(_turretPanelPrefab, _turretPanelContainer);
            _turretPanelList.Add(newPanel);
            newPanel.GetComponent<TurretPanel>().Initialize(this, newTurret.GetComponent<TurretBaker>());
        }
    }

    public void PassPartyState(PartyState state)
    {
        if (state == PartyState.TowerDefencePlacement)
        {
            foreach (var item in _turretPanelList)
                item.GetComponent<TurretPanel>().OpenPanel(false);
        }
    }

    private void Update()
    {
        if (!_gostTurret) return;
        _gostTurret.transform.position = Vector3.Lerp(_gostTurret.transform.position, GetGridMousePosition(_gostTurret.transform.position) + Vector3.up, Time.deltaTime * 10);
    }

    public void OnGostTurretPlacement()
    {
        if (!_gostTurret) return;
        _gostTurret.transform.position = GetGridMousePosition(_gostTurret.transform.position);
        _turretPlacedList.Add(_gostTurret.GetComponent<TurretBaker>());
        _gostTurret = null;
    }

    public void ClicPanel(TurretPanel panel)
    {
        if (PartyManager.instance.GameState == PartyState.TowerDefencePlacement)
        {
            // if (_turretPlacedList.Contains(panel.TurretBaker)) return;
            if (_gostTurret)
            {
                _gostTurret.SetActive(false);
            }
            _gostTurret = panel.TurretBaker.gameObject;
            _gostTurret.SetActive(true);
            _lastPanelOpen = panel;
            return;
        }

        if (panel == _lastPanelOpen)
        {
            panel.OpenPanel(false);
            _lastPanelOpen = null;
            return;
        }

        foreach (var item in _turretPanelList)
            item.GetComponent<TurretPanel>().OpenPanel(false);

        panel.GetComponent<TurretPanel>().OpenPanel(true);
        _lastPanelOpen = panel;
    }

    private Vector3 GetGridMousePosition(Vector3 current)
    {
        RaycastHit[] hits = Physics.RaycastAll(_mainCam.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, _groundLayer);
        if (hits.Length == 0) return current;
        return _grid.WorldToCell(hits[0].point);
    }

    private void OnClic(InputValue value)
    {
        if(value.Get<float>() > .5f)
            OnGostTurretPlacement();
    }
}
