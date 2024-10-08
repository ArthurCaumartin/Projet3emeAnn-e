using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask _mobLayer;
    private float _speed;
    private float _damage;
    private float _perforateCount;

    public Projectile Initialize(float speed, float damage, int perforateCount)
    {
        _speed = speed;
        _damage = damage;
        _perforateCount = perforateCount;
        Destroy(gameObject, 2f);
        return this;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        MobHealth mobHit = other.GetComponent<MobHealth>();
        if (mobHit)
        {
            HitMob(mobHit);
        }
    }

    private void HitMob(MobHealth mobHit)
    {
        // print("Hit Mob !");
        mobHit.DoDamage(_damage);
        _perforateCount--;
        if (_perforateCount <= -1) Destroy(gameObject);
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
    }
}