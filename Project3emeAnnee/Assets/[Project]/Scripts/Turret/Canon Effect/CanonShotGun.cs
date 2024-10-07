using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonShotGun : TurretCanon
{
    public override void Shoot()
    {
        for (int i = 0; i < _stat.bulletCount; i++)
        {
            float loopTime = Mathf.Lerp(-1, 1, Mathf.InverseLerp(0, _stat.bulletCount, i));
            Vector3 newDirection = new Vector3(loopTime, 0, 1);
            newDirection = transform.rotation * newDirection.normalized;
            newDirection.y = 0;

            Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(newDirection, Vector3.up));
            newProjectile.Initialize(_stat.projectileSpeed, _stat.damage, _stat.perforationCount);
        }
    }
}