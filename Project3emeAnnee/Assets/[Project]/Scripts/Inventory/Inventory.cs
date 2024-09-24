using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _inventorySize = 12;
    [SerializeField] private float _itemAnimationSpeed = 5;
    [Header("UI Reference :")]
    [SerializeField] private Transform _inventorySlotContainer;
    [SerializeField] private Transform _inventoryItemContainer;
    [SerializeField] private GameObject _inventorySlotPrefab;
    private List<InventoryItem> _inventoryItemList = new List<InventoryItem>();
    private InventorySlot[] _inventoryArray;

    [Header("To Move away :")]
    [SerializeField] private GameObject _itemPrefab;
    private InventoryItem _dragItem;
    public InventoryItem DragItem
    {
        get => _dragItem;
        set
        {
            //! Disable target to enable inventory event
            foreach (var item in _inventoryItemList)
                item.SetRaycastTarget(!value);
            _dragItem = value;
        }
    }

    private void Start()
    {
        DrawInventory();
    }

    private void Update()
    {
        foreach (var item in _inventoryArray)
            item.UpdateItemPos(_itemAnimationSpeed);

        if (_dragItem)
        {
            _dragItem.transform.position = Input.mousePosition;
        }
    }

    public void DrawInventory()
    {
        if (_inventoryArray != null && _inventoryArray.Length > 0)
        {
            print("clear inventory slot");
            for (int i = 0; i < _inventoryArray.Length; i++)
                DestroyImmediate(_inventoryArray[i]);
        }

        _inventoryArray = new InventorySlot[_inventorySize];
        for (int i = 0; i < _inventoryArray.Length; i++)
        {
            GameObject newSlot = Instantiate(_inventorySlotPrefab, transform.position, Quaternion.identity, _inventorySlotContainer);
            _inventoryArray[i] = newSlot.GetComponent<InventorySlot>().Initialize(this);
        }
    }

    public void SpawnNewItem()
    {
        GameObject newItem = Instantiate(_itemPrefab, _inventoryItemContainer.transform.position, Quaternion.identity, _inventoryItemContainer);
        InventoryItem newInvItem = newItem.GetComponent<InventoryItem>();
        AddItemToInventory(newInvItem);
    }

    public void AddItemToInventory(InventoryItem item)
    {
        item.Initialize(this);
        // print("Add item");
        for (int i = _inventoryArray.Length; i > 1; i--)
        {
            // print(i);
            _inventoryArray[i - 1].Item = _inventoryArray[i - 2].Item;
        }
        _inventoryArray[0].Item = item;
        _inventoryItemList.Add(item);
    }

    public void ItemGrabOverSlot()
    {

    }
}

[CustomEditor(typeof(Inventory)), CanEditMultipleObjects]
public class EditorInventory : Editor
{
    public override void OnInspectorGUI()
    {
        Inventory inventory = (Inventory)target;
        if (GUILayout.Button("Generate Inventory")) inventory?.DrawInventory();
        if (GUILayout.Button("Spawn New Item")) inventory?.SpawnNewItem();

        base.OnInspectorGUI();
    }
}
