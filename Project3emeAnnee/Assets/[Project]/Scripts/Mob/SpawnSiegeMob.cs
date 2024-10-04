using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnSiegeMob : MonoBehaviour
{
    public List<Spawners> _spawners = new List<Spawners>();
    public Button _startButton;
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = PlayerInstance.instance.transform;
        _startButton.onClick.AddListener(StartWaves);
    }

    public void StartWaves()
    {
        for (int i = 0; i < _spawners.Count; i++)
        {
            _spawners[i].SetPlayerTransform(_playerTransform);
            _spawners[i].StartSpawnerWaves(this);
        }
    }

    public void FinishWave(Spawners finishedSpawner)
    {
        _spawners.Remove(finishedSpawner);
        if (_spawners.Count == 0)
        {
            FinishExtracting();
        }
    }

    public void FinishExtracting()
    {
        GameData.instance.AddGas(50 + (50 * GameData.instance.Difficulty));
        
        GameData.instance.ChangeDifficulty(true);
    }
}


