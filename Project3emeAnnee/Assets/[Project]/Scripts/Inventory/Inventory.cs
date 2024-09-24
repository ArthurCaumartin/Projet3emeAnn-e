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
    [SerializeField] private InventorySlot[] _inventorySlotArray;

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
        foreach (var item in _inventorySlotArray)
            item.UpdateItemPos(_itemAnimationSpeed);

        if (_dragItem)
        {
            _dragItem.transform.position = Input.mousePosition;
        }
    }

    public void DrawInventory()
    {
        if (_inventorySlotArray != null && _inventorySlotArray.Length > 0)
        {
            print("clear inventory slot");
            for (int i = 0; i < _inventorySlotArray.Length; i++)
                DestroyImmediate(_inventorySlotArray[i]);
        }

        _inventorySlotArray = new InventorySlot[_inventorySize];
        for (int i = 0; i < _inventorySlotArray.Length; i++)
        {
            GameObject newSlot = Instantiate(_inventorySlotPrefab, transform.position, Quaternion.identity, _inventorySlotContainer);
            _inventorySlotArray[i] = newSlot.GetComponent<InventorySlot>().Initialize(this);
        }
    }

    public void SpawnNewItem()
    {
        GameObject newItem = Instantiate(_itemPrefab, _inventoryItemContainer.transform.position, Quaternion.identity, _inventoryItemContainer);
        InventoryItem newInvItem = newItem.GetComponent<InventoryItem>();
        newInvItem.Initialize(this);
        AddItemToInventory(newInvItem);
    }

    public void TrashItem(InventoryItem itemToTrash)
    {
        print("Trash");
        _inventoryItemList.Remove(itemToTrash);
        itemToTrash.TrashAnimation();
    }

    public void AddItemToInventory(InventoryItem item)
    {
        for (int i = _inventorySlotArray.Length - 1; i > 0; i--)
        {
            if (i == _inventorySlotArray.Length - 1 && _inventorySlotArray[i].Item)
            {
                TrashItem(_inventorySlotArray[i].Item);
            }
            _inventorySlotArray[i].Item = _inventorySlotArray[i - 1].Item;
        }
        _inventorySlotArray[0].Item = item;
        _inventoryItemList.Add(item);
    }

    public void ItemGrabOverSlot(InventorySlot slotOver)
    {
        if (slotOver.Item) return;
        foreach (var item in _inventorySlotArray)
        {
            if (item.Item == DragItem) item.Item = null;
        }
        slotOver.Item = DragItem;
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
