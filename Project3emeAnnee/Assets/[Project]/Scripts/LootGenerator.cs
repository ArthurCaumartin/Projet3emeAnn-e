using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootGenerator : MonoBehaviour
{
    public static LootGenerator instance;
    [Tooltip("Roll a dice between 0 and 100 if drop under _drop chance Generate a Item")]
    [SerializeField, Range(0, 100)] private float _dropChance; //TODO Move to game data ?
    [SerializeField] private Inventory _inventory;
    [SerializeField] private TurretPartDescritor _itemPrefab;
    [Space]
    [SerializeField] private List<ScriptableTurretPart> _turretPartList = new List<ScriptableTurretPart>();
    private ScriptableGameData _gameData;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _gameData = GameManager.instance.GetGameData();
    }

    public void GenerateItem(float dropRateBonus = 0)
    {
        if (Random.value > (_dropChance / 100) + dropRateBonus || _turretPartList.Count == 0) return;

        TurretPartDescritor newItem = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
        newItem.TurretPart = _turretPartList[Random.Range(0, _turretPartList.Count)];
        _inventory.AddItemToInventory(newItem.GetComponent<InventoryItem>().Initialize(_inventory));
    }
}

