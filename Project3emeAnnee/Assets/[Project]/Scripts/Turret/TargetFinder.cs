using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private List<Mob> _mobInRange = new List<Mob>();
    public float _range;
    private SphereCollider _collider;
    private StatContainer _stat;
    public StatContainer Stat
    {
        set
        {
            _stat = value;
            Bake();
        }
    }

    private void Bake()
    {
        if(!_collider) _collider = GetComponent<SphereCollider>();
        _collider.radius = _stat.range;
    }

    public Mob GetNearsetMob()
    {
        Mob toReturn = null;
        float minDistance = Mathf.Infinity;
        foreach (var item in _mobInRange)
        {
            float currentDistance = (item.transform.position - transform.position).sqrMagnitude;
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                toReturn = item;
            }
        }
        return toReturn;
    }

    private void OnTriggerEnter(Collider other)
    {
        Mob mob = other.GetComponent<Mob>();
        if (mob)
        {
            _mobInRange.Add(mob);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Mob mob = other.GetComponent<Mob>();
        if (mob && _mobInRange.Contains(mob))
        {
            _mobInRange.Remove(mob);
        }
    }
}
