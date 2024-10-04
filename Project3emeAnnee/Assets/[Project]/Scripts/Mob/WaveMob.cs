using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WavesSpawnTD", fileName = "waveSpawnTD", order = 4)]
public class WaveMob : ScriptableObject
{
    public List<Wave> waves = new List<Wave>();
    public string name;
    public float duration;
    public int reward;
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

// public enum MobClassName {
//     Basic,
//     Bomber,
//     Tank,
//     Fast
// }

[Serializable]
public class MobToSpawn
{
    public string mobName;
    public int mobNumber;
}