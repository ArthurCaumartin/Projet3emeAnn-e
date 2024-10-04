using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Mob : MonoBehaviour
{
    public string _name;
    public int _health;
    public float _movementSpeed;
    public float _mobScale;

    public UnityEvent OnDeath;
    
    private Transform _player;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public Mob Initialize(Transform player)
    {
        _player = player;
        return this;
    }

    private void Update()
    {
         _navMeshAgent.destination = _player.position;
    }

    // public void OnCollisionEnter(Coll other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         _spawnMobileMob.DeleteAMob(gameObject);
    //     }
    // }
}
