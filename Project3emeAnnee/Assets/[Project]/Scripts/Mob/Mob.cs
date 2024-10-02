using System;
using UnityEngine;
using UnityEngine.AI;

public class Mob : MonoBehaviour
{
    public Transform _player;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    private SpawnMobileMob _spawnMobileMob;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _spawnMobileMob = GetComponentInParent<SpawnMobileMob>();

        _player = _spawnMobileMob._playerPos;
        _navMeshAgent.destination = _player.position;
    }

    private void Update()
    {
        _navMeshAgent.destination = _player.position;
    }

    public void OnCollisionEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // _spawnMobileMob.DeleteAMob(gameObject);
        }
    }
}
