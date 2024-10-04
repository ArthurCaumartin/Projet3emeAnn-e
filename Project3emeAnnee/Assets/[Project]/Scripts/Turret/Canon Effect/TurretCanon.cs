using UnityEngine;

public class TurretCanon : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    private Transform _currentTarget;
    private float _shootTime = 0;
    private TargetFinder _finder;
    private StatContainer _stat;

    public virtual void Shoot()
    {
        // print(name + " SHOOT!");     
        Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        newProjectile.Initialize(_stat.projectileSpeed, _stat.damage, _stat.perforationCount);
    }

    public TurretCanon Inistalize(TargetFinder finder, StatContainer stat)
    {
        _finder = finder;
        _stat = stat;
        return this;
    }

    public void Update()
    {
        _currentTarget = _finder.GetNearsetMob()?.transform;
        if (!_currentTarget) return;
        transform.LookAt(_currentTarget);
        _shootTime += Time.deltaTime;
        if (_shootTime > 1 / _stat.attackPerSecond)
        {
            _shootTime = 0;
            Shoot();
        }
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }
}
