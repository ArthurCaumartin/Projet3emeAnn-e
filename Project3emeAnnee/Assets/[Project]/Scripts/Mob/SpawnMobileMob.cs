using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SpawnMobileMob : MonoBehaviour
{
    public Transform _playerPos;
    private float _cooldown;
    public float _maxDistance = 2f, _cooldownSpawn = 1f;
    public BoxCollider _outerBoxSpawn, _interBoxSpawn;
    public GameObject _enemyPrefab;
    
    private void Update()
    {
        _cooldown += Time.deltaTime;

        if (_cooldown >= _cooldownSpawn)
        {
            GetRandomPos();
            _cooldown = 0;
        }
    }

    private void GetRandomPos()
    {
        float xMinPosOuter = _playerPos.position.x - (_outerBoxSpawn.size.x / 2);
        float xMaxPosOuter = _playerPos.position.x + (_outerBoxSpawn.size.x / 2);
        float zMinPosOuter = _playerPos.position.z - (_outerBoxSpawn.size.z / 2);
        float yMaxPosOuter = _playerPos.position.z + (_outerBoxSpawn.size.z / 2);
        
        float xPosInter = _playerPos.position.x + (_interBoxSpawn.size.x / 2);
        float zPosInter = _playerPos.position.z + (_interBoxSpawn.size.z / 2);

        float xPosSpawn = Random.Range(xMinPosOuter, xMaxPosOuter);
        float zPosSpawn = Random.Range(zMinPosOuter, yMaxPosOuter);
        
        while(!(xPosSpawn <= -xPosInter || xPosSpawn >= xPosInter || zPosSpawn >= zPosInter || zPosSpawn <= -zPosInter)){
            xPosSpawn = Random.Range(xMinPosOuter, xMaxPosOuter);
            zPosSpawn = Random.Range(zMinPosOuter, yMaxPosOuter);
        }
        
        Vector3 enemySpawnPosition = new Vector3(xPosSpawn, 2, zPosSpawn);
        
        NavMeshHit hit;

        if (NavMesh.SamplePosition(enemySpawnPosition, out hit, _maxDistance, NavMesh.AllAreas))
        {
            SpawnMob(enemySpawnPosition);
        }
        else
        {
            GetRandomPos();
        }
    }

    private void SpawnMob(Vector3 spawnPoistion)
    {
        GameObject enemyInstantiate = Instantiate(_enemyPrefab, spawnPoistion, Quaternion.identity, transform);
    }
    
    
}
