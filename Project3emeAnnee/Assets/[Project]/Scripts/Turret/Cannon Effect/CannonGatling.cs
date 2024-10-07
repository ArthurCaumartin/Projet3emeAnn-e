using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonGatling : TurretCannon
{
    [SerializeField] private float _numberBulletsPerShot = 3;
    private float _counterTime, _counterBullets;
    private bool _shooting;

    public override void Update()
    {
        base.Update();
        if (_shooting)
        {
            _counterTime += Time.deltaTime;
            if(_counterTime >= )
        }
    }

    public override void Shoot()
    {
        _shooting = true;
        _counterTime = 0;
    }

    public void TrueShoot()
    {
        Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        newProjectile.Initialize(_stat.projectileSpeed, _stat.damage, _stat.perforationCount);
    }
}
