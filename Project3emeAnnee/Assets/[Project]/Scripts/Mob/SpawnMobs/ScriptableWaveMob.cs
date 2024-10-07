using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WavesSpawnTD", fileName = "waveSpawnTD", order = 4)]
public class ScriptableWaveMob : ScriptableObject
{
    public List<Wave> waves = new List<Wave>();
    public string name;
}

[Serializable]
public class Wave
{
    public string name;
    public int spawnTimeInWaveMob;
    public float spawnDuration;
    public List<MobToSpawn> mobsToSpawn = new List<MobToSpawn>();
}

[Serializable]
public class MobsToSpawn
{
    public List<MobToSpawn> mobs = new List<MobToSpawn>();
}

[Serializable]
public class MobToSpawn
{
    public GameObject mobName;
    public int mobNumber;
}