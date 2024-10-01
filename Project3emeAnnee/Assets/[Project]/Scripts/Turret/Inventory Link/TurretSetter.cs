using System.Collections.Generic;
using UnityEngine;

public class TurretSetter : MonoBehaviour
{
    [SerializeField] private TurretBaker _turret;
    public TurretBaker TurretBaker { set => _turret = value; }

    public void ChangeTurretPart(ScriptableTurretPart part)
    {
        _turret.SetTurretComponent(part);
    }
}
