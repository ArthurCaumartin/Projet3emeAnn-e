using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private List<Mob> _mobInRangeList = new List<Mob>();
    public float _range;
    private SphereCollider _collider;
    private StatContainer _stat;
    private TurretCannon _cannon;

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = _range;
    }

    public void Bake(TurretCannon newCannon, StatContainer newStat)
    {
        if (_cannon) Destroy(_cannon.gameObject);

        _stat = newStat;
        _cannon = Instantiate(newCannon, transform.position, Quaternion.identity, transform);
        _cannon.Inistalize(this, _stat);
        if (!_collider) _collider = GetComponent<SphereCollider>();
        _collider.radius = _stat.range;
    }

    public Mob GetNearsetMob()
    {
        if (_mobInRangeList.Count == 0) return null;

        Mob toReturn = null;

        float minDistance = Mathf.Infinity;
        foreach (var item in _mobInRangeList)
        {
            if (!item) continue; //TODO remove mob on death
            float currentDistance = (item.transform.position - transform.position).sqrMagnitude;
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                toReturn = item;
            }
        }
        // print("GetNearsetMob return : " + toReturn?.name);
        return toReturn;
    }

    private void OnTriggerEnter(Collider other)
    {
        Mob mob = other.GetComponent<Mob>();
        if (mob)
        {
            mob.GetComponent<MobHealth>().OnDeathEvent.AddListener(RemoveMob);
            _mobInRangeList.Add(mob);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Mob mob = other.GetComponent<Mob>();
        if (mob && _mobInRangeList.Contains(mob))
        {
            _mobInRangeList.Remove(mob);
        }
    }

    public void RemoveMob(Mob toRemove)
    {
        if (_mobInRangeList.Contains(toRemove))
            _mobInRangeList.Remove(toRemove);
    }
}
