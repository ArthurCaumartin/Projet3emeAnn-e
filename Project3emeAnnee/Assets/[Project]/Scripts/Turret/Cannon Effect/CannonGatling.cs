using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CannonGatling : TurretCannon
{
    [SerializeField] private float _attackSpeedMultiplier = 1.4f, _damagesMultiplier = 0.8f, _timeToDecreaseCharge = 0.25f;
    
    [SerializeField, Range(0, 90)] private float _angleShootAtMaxCharge = 35;

    [SerializeField] private int _bulletsMaxCharge = 10;
    private int _bulletCounter = 0;
    private float _counterDecrease;
    
    public override void Update()
    {
        base.Update();

        _counterDecrease += Time.deltaTime;
        if (_counterDecrease >= _timeToDecreaseCharge)
        {
            _bulletCounter--;
            _counterDecrease = 0;
        } 
        
    }
    public override void Shoot()
    {
        float fullSpreadMultiplier = Mathf.InverseLerp(0, _bulletsMaxCharge, _bulletCounter);
        
        float tempAngleTime = Mathf.InverseLerp(0, 90, _angleShootAtMaxCharge * fullSpreadMultiplier);
        float tempAngleFloat = Mathf.Lerp(-tempAngleTime, tempAngleTime, Random.value);
        
        Vector3 newDirection = new Vector3(tempAngleFloat, 0, 1);
        newDirection = transform.rotation * newDirection.normalized;
        newDirection.y = 0;
        
        Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(newDirection, Vector3.up));
        newProjectile.Initialize(_stat.projectileSpeed, _stat.damage * _damagesMultiplier, _stat.perforationCount);

        _bulletCounter++;
        _counterDecrease = 0;
    }
    
    protected override void ComputeShootTime()
    {
        float fullShootSpeedMultiplier = Mathf.Lerp(0.6f, 1.35f, Mathf.InverseLerp(0, _bulletsMaxCharge, _bulletCounter));
        
        _shootTime += Time.deltaTime;
        if (_shootTime > 1 / (_stat.attackPerSecond * (_attackSpeedMultiplier * fullShootSpeedMultiplier)))
        {
            _shootTime = 0;
            Shoot();
        }
    }
}
