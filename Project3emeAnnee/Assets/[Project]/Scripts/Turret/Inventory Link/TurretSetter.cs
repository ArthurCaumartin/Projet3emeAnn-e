using System.Collections.Generic;
using UnityEngine;

public class TurretSetter : MonoBehaviour
{
    [SerializeField] private GameObject _turret;

    public void ChangeTurretPart(ScriptableTurretPart part)
    {
        _turret.GetComponent<TurretBaker>().SetTurretComponent(part);
    }
}
