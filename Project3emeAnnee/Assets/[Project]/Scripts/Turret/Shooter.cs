using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private TurretCanon _cannon;
    private TargetFinder _finder;
    private Mob _mob;
    private StatContainer _stat;

    public void SetCannon(TurretCanon toAdd, StatContainer stat) 
    {
        if (_cannon) Destroy(_cannon);
        _cannon = gameObject.AddComponent(toAdd.GetType()) as TurretCanon;
        _stat = stat;
    }

    private void Update()
    {
        if(!_mob)
        {
            _mob = _finder.GetNearsetMob();
        }

        if(_mob)
        {
            
        }
    }
}