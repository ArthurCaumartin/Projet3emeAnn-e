using UnityEngine;

public class TurretCanon : MonoBehaviour
{
    [SerializeField] protected Projectile _projectilePrefab;
    protected Transform _currentTarget;
    protected float _shootTime = 0;
    protected TargetFinder _finder;
    protected StatContainer _stat;

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

    public virtual void Update()
    {
        print("Virtual Void");
        _currentTarget = _finder.GetNearsetMob()?.transform;
        if (!_currentTarget) return;
        LookAtTarget();
        ComputeShootTime();
    }

    private void ComputeShootTime()
    {
        _shootTime += Time.deltaTime;
        if (_shootTime > 1 / _stat.attackPerSecond)
        {
            _shootTime = 0;
            Shoot();
        }
    }

    private void LookAtTarget()
    {
        transform.LookAt(_currentTarget);
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }
}
