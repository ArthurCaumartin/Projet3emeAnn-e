using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [Header("Turret Panel : ")]
    [SerializeField] private GameObject _turretPanelPrefab;
    [SerializeField] private RectTransform _turretPanelContainer;
    [Space]
    [Header("Turret : ")]
    [SerializeField] private GameObject _turretPrefab;
    private List<GameObject> _turretPanelList = new List<GameObject>();
    private List<GameObject> _turretList = new List<GameObject>();

    void Start()
    {
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
            newPanel.GetComponent<TurretSetter>().TurretBaker = newTurret.GetComponent<TurretBaker>();
        }
    }
}
