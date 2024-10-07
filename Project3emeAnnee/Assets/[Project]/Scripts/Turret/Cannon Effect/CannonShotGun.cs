using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShotGun : TurretCannon
{
    [SerializeField] private int _numberBullets = 8;
    
    public override void Shoot()
    {
        for (int i = 0; i < _numberBullets; i++)
        {
            float loopTime = Mathf.Lerp(-1, 1, Mathf.InverseLerp(0, _numberBullets, i));
            Vector3 newDirection = new Vector3(loopTime, 0, 1);
            newDirection = transform.rotation * newDirection.normalized;
            newDirection.y = 0;

            Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(newDirection, Vector3.up));
            newProjectile.Initialize(_stat.projectileSpeed, _stat.damage * _damagesMultiplier, _stat.perforationCount);
        }
    }
    
    protected override void ComputeShootTime()
    {
        _shootTime += Time.deltaTime;
        if (_shootTime > 1 / (_stat.attackPerSecond * _attackSpeedMultiplier))
        {
            _shootTime = 0;
            Shoot();
        }
    }
}
