using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonShotGun : TurretCanon
{
    public override void Shoot()
    {
        for (int i = 0; i < _stat.bulletCount; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection = Vector3.Slerp(transform.forward, randomDirection, Random.Range(0.0f, Mathf.Sin(35f * Mathf.Deg2Rad)));
            randomDirection.y = 0;
           
            Quaternion newOrientation = Quaternion.LookRotation(randomDirection, Vector3.up);
            Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, newOrientation);
            newProjectile.Initialize(_stat.projectileSpeed, _stat.damage, _stat.perforationCount);
        }
    }
}