using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "GameData", fileName = "GameData", order = 5)]
public class ScriptableGameData : ScriptableObject
{
    public int globalGas = 0;
    [Space]
    public int currentDifficultyIndex = 1;
    // public List<Difficulty> difficultyList = new List<Difficulty>();
    [Space]
    [SerializedDictionary("OptiName", "Value")]
    public SerializedDictionary<string, int> mechaOptimisations = new SerializedDictionary<string, int>();
}

[Serializable]
public struct Difficulty
{ //TODO CAMILLE ! ALED
    public int difficultyIndex;
    public List<RarityDropRate> dropRateList;
    [Serializable]
    public struct RarityDropRate
    {
        public string name;
        public float dropRate;
        public float turretStatMultiplier;
    }
}