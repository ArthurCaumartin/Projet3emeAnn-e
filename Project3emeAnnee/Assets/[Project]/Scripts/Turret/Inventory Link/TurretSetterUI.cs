using System.Collections.Generic;
using UnityEngine;

public class TurretSetterUI : MonoBehaviour
{
    [SerializeField] private TurretBaker _turretBaker;
    [SerializeField] private List<TurretPartDescritor> _turretDescriptorList = new List<TurretPartDescritor>();

    public void SetTurretBaker()
    {
        for (int i = 0; i < _turretDescriptorList.Count; i++)
        {
            _turretBaker.SetTurretComponent(_turretDescriptorList[i].GetItem());
        }
    }
}
