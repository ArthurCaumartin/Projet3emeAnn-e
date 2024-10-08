using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnSiegeMob : MonoBehaviour
{
    public List<Spawners> _spawners = new List<Spawners>();
    public Button _startButton;
    private Transform _playerTransform;
    [SerializeField] private UnityEvent _onWaveEnd;
    public UnityEvent OnWaveEnd { get => _onWaveEnd; }

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
        print("Shipon finish Call Event !");
        _onWaveEnd.Invoke();
    }
}


