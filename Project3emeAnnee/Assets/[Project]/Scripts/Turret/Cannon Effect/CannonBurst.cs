using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBurst : TurretCannon
{
    [SerializeField] private float _numberBulletsPerShot = 3;
    [SerializeField] private float _timeBetweenBullets = 0.1f;
    private float _counterTime = 0, _counterBullets = 0;
    private bool _shooting = false;

    public override void Update()
    {
        base.Update();
        if (_shooting)
        {
            _counterTime += Time.deltaTime;
            if (_counterTime >= _timeBetweenBullets)
            {
                TrueShoot();
            }
        }
    }

    public override void Shoot()
    {
        _shooting = true;
        _counterTime = _timeBetweenBullets;
    }

    public void TrueShoot()
    {
        _counterTime = 0;
        _counterBullets++;
        if (_counterBullets >= _numberBulletsPerShot) _shooting = false;
        
        Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        newProjectile.Initialize(_stat.projectileSpeed, _stat.damage, _stat.perforationCount);
    }
}
