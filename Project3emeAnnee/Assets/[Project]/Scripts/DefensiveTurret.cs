using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveTurret : MonoBehaviour
{
    [SerializeField] private Transform _playerInRange;
    public float _range;
    private SphereCollider _collider;
    private StatContainer _stat;
    private TurretCannon _cannon;

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = _range;
    }

    // public void Bake(TurretCannon newCannon, StatContainer newStat)
    // {
    //     if (_cannon) Destroy(_cannon.gameObject);
    //
    //     _stat = newStat;
    //     _cannon = Instantiate(newCannon, transform.position, Quaternion.identity, transform);
    //     _cannon.Inistalize(this, _stat);
    //     if (!_collider) _collider = GetComponent<SphereCollider>();
    //     _collider.radius = _stat.range;
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = null;
        }
    }
}
