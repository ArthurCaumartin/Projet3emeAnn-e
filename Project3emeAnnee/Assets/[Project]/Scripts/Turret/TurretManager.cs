using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [Header("Turret Panel : ")]
    [SerializeField] private GameObject _turretPanelPrefab;
    [SerializeField] private RectTransform _turretPanelContainer;
    [Space]
    [Header("Turret : ")]
    [SerializeField] private TurretBaker _mobileTurretPrefab;
    [SerializeField] private TurretBaker _turretPrefab;
    private List<TurretPanel> _turretPanelList = new List<TurretPanel>();
    private List<TurretBaker> _turretList = new List<TurretBaker>();
    private List<TurretBaker> _turretMobileList = new List<TurretBaker>();
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
            TurretBaker newTurret = Instantiate(_turretPrefab, transform);
            _turretList.Add(newTurret);
            newTurret.gameObject.SetActive(false);

            TurretBaker newMobileTurret = Instantiate(_mobileTurretPrefab, transform);
            newMobileTurret.GetComponent<MobileTurret>().Initialize(PlayerInstance.instance.transform, Mathf.InverseLerp(0, count, i));
            _turretMobileList.Add(newMobileTurret);

            GameObject newPanel = Instantiate(_turretPanelPrefab, _turretPanelContainer);
            _turretPanelList.Add(newPanel.GetComponent<TurretPanel>().Initialize(this, newTurret, newMobileTurret));
        }
    }

    public void PassPartyState(PartyState state)
    {
        if (state == PartyState.TowerDefencePlacement || state == PartyState.TowerDefence)
        {
            foreach (var item in _turretMobileList)
                item.gameObject.SetActive(false);

            foreach (var item in _turretPanelList)
            {
                item.OpenPanel(false);
                item.ShowPlacementButton(true);
            }
            return;
        }

        foreach (var item in _turretMobileList)
            item.gameObject.SetActive(true);

        foreach (var item in _turretList)
            item.gameObject.SetActive(false);

        foreach (var item in _turretPanelList)
            item.ShowPlacementButton(false);
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

    public void ClicOnPlacementButton(TurretPanel panel)
    {
        if (_gostTurret)
        {
            _gostTurret.SetActive(false);
        }
        _gostTurret = panel.TurretBaker.gameObject;
        _gostTurret.SetActive(true);
        _lastPanelOpen = panel;
        return;
    }

    public void ShowPanel(TurretPanel panel)
    {
        // if (panel == _lastPanelOpen)
        // {
        //     panel.OpenPanel(false);
        //     _lastPanelOpen = null;
        //     return;
        // }

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
        if (value.Get<float>() > .5f)
            OnGostTurretPlacement();
    }
}
