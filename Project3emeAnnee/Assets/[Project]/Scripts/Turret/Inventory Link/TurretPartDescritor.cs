using System.Collections.Generic;
using UnityEngine;

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
    }

    public ScriptableTurretPart GetItem()
    {
        return _turetPart;
    }
}
