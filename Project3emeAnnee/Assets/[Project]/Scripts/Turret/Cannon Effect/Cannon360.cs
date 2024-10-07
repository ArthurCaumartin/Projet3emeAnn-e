using UnityEngine;

public class Cannon360 : TurretCannon
{
    [SerializeField] private float _rotateSpeedMultiplier = 5;
    [SerializeField] private int _initialAdditionalBullet;
    public override void Update()
    {
        transform.Rotate(new Vector3(0, _rotateSpeedMultiplier * _stat.rotateSpeed * Time.deltaTime, 0));
        _currentTarget = _finder.GetNearsetMob()?.transform;
        if (!_currentTarget) return;
        ComputeShootTime();
    }

    public override void Shoot()
    {
        int totalBullet = _stat.bulletCount + _initialAdditionalBullet;
        for (int i = 0; i < totalBullet; i++)
        {
            float angleTime = Mathf.Lerp(0, 360, Mathf.InverseLerp(0, totalBullet, i));
            print(angleTime);
            Vector3 newDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angleTime), 0, Mathf.Cos(Mathf.Deg2Rad * angleTime));
            print(newDirection);
            newDirection = transform.rotation * newDirection;
            Projectile newProj = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(newDirection, Vector3.up));
            newProj.Initialize(_stat.projectileSpeed, _stat.damage, _stat.perforationCount);
        }
    }
}


public static class MathfEx
{
    public static float Remap(float inMin, float inMax, float outMin, float ouMax, float time)
    {
        return Mathf.Lerp(outMin, ouMax, Mathf.InverseLerp(inMin, inMax, time));
    }
}