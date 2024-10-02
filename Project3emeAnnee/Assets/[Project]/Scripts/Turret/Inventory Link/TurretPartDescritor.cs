using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPartDescritor : MonoBehaviour
{
    [SerializeField] private ScriptableTurretPart _turetPart;
    public List<ScriptableTurretPart> _turretPartList = new List<ScriptableTurretPart>();

    private void Start()
    {
        GenerateItem();
    }

    private void GenerateItem()
    {
        _turetPart = _turretPartList[Random.Range(0, _turretPartList.Count)];
        GetComponent<Image>().sprite = _turetPart.inventorySprite;
    }

    public ScriptableTurretPart GetTurretPart()
    {
        return _turetPart;
    }
}
