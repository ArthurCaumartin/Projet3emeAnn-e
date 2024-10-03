using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public Transform _posToSpawn;
    public WaveMob _wavesToSpawn;

    private bool hasStarted, hasFinished;
    private float duration;
    
    private int actualWave = 0;
    private void Start()
    {
        _posToSpawn = transform;
    }

    public void StartSpawnerWaves()
    {
        hasStarted = true;
    }

    public void Update()
    {
        if (!hasStarted) return;
        
        duration += Time.deltaTime;

        if (duration >= _wavesToSpawn.waves[actualWave].spawnTimeInWaveMob)
        {
            InstantiateWave(actualWave);
            actualWave++;
        }
    }

    public void InstantiateWave(int waveNumber)
    {
        Dictionary<string, int> mobsToSpawn = new Dictionary<string, int>();
        
        for (int i = 0; i < _wavesToSpawn.waves[waveNumber].mobsToSpawn.Count; i++)
        {
            mobsToSpawn[_wavesToSpawn.waves[waveNumber].mobsToSpawn[i].mobName] = _wavesToSpawn.waves[waveNumber].mobsToSpawn[i].mobNumber;
        }

        foreach (var mobClassNumber in mobsToSpawn)
        {
            for (int i = 0; i < mobClassNumber.Value; i++)
            {
                Vector3 randomPosToSpawn = GetRandomSpawnPos(transform.position);
                // Instantiate(mobClassNumber.Key, randomPosToSpawn, Quaternion.identity, transform);
            }
        }
    }

    private Vector3 GetRandomSpawnPos(Vector3 position)
    {
        float xRandomPos = Random.Range(position.x - 10, position.x + 10);
        float zRandomPos = Random.Range(position.z - 10, position.z + 10);

        return new Vector3(xRandomPos, position.y, zRandomPos);
    }
}
