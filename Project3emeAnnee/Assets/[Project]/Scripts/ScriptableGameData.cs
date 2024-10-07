using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData", fileName = "GameData", order = 5)]
public class ScriptableGameData : ScriptableObject
{
    public int globalGas = 0;

    public int difficulty = 1;

    [SerializedDictionary("OptiName", "Value")]
    public SerializedDictionary<string, int> mechaOptimisations = new SerializedDictionary<string, int>();
}
    