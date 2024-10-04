using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSiegeMob : MonoBehaviour
{
    public List<Spawners> _spawners = new List<Spawners>();
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = PlayerInstance.instance.transform;
    }

    public void StartWaves()
    {
        for (int i = 0; i < _spawners.Count; i++)
        {
            _spawners[i].SetPlayerTransform(_playerTransform);
            _spawners[i].StartSpawnerWaves();
        }
    }
}


