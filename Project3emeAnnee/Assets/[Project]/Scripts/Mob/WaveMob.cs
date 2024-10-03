using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMob : ScriptableObject
{
    public List<Waves> waves = new List<Waves>();
}

[Serializable]
public class Waves
{
    public string name;
    public float duration;
    public List<MobToSpawn> mobsToSpawn = new List<MobToSpawn>();
}

[Serializable]
public class MobToSpawn
{
    public List<GameObject> mobs = new List<GameObject>();
}
