using System;
using UnityEngine;
using UnityEngine.AI;

public class Mob : MonoBehaviour
{
    public string _name;
    public int _health;
    public float _movementSpeed;
    public float _mobScale;
    
    private Transform _player;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.destination = _player.position;
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
