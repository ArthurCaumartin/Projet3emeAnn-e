using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSiegeMob : MonoBehaviour
{
    public List<Spawners> _spawners = new List<Spawners>();
    public Transform _playerPos;
    public void StartWaves()
    {
        for (int i = 0; i < _spawners.Count; i++)
        {
            _spawners[i].StartSpawnerWaves();
        }
    }
}


