using System;
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
            newSlot.name = "Slot_" + i;
            _inventorySlotArray[i] = newSlot.GetComponent<InventorySlot>().Initialize(this);
        }
    }

    public void SpawnNewItem()
    {
        GameObject newItem = Instantiate(_itemPrefab, _inventoryItemContainer.transform.position, Quaternion.identity, _inventoryItemContainer);
        newItem.name = "Item_" + _inventoryItemList.Count;
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
        DragItemRight(_inventorySlotArray.Length - 1, 0);
        _inventorySlotArray[0].Item = item;
        _inventoryItemList.Add(item);
    }

    private void DragItemRight(int start, int end)
    {
        print("Go to Right");
        for (int i = start; i > end; i--)
        {
            if (i == _inventorySlotArray.Length - 1 && _inventorySlotArray[i].Item)
            {
                TrashItem(_inventorySlotArray[i].Item);
            }
            _inventorySlotArray[i].Item = _inventorySlotArray[i - 1].Item;
        }
    }

    private void DragItemLeft(int fromIndex, int toIndex)
    {
        for (int i = fromIndex; i < toIndex; i++)
        {
            _inventorySlotArray[i].Item = _inventorySlotArray[i + 1].Item;
            if (i == toIndex - 1)
            {
                _inventorySlotArray[i + 1].Item = null;
            }
        }
    }

    public void ItemGrabOverSlot(InventorySlot slotOver)
    {
        if (!DragItem) return;
        if (slotOver.Item == DragItem)
        {
            print("Over slot item is Drag Item !");
            return;
        }

        int dragItemIndex = GetDragItemIndex();
        int overIndex = Array.IndexOf(_inventorySlotArray, slotOver);

        if (slotOver.Item)
        {
            print("drag Index : " + dragItemIndex + " /// over Index : " + overIndex);
            if (dragItemIndex < overIndex) DragItemLeft(dragItemIndex, overIndex);
            if (dragItemIndex > overIndex) DragItemRight(dragItemIndex, overIndex);

            slotOver.Item = DragItem;
            return;
        }

        if (!slotOver.Item)
        {
            print("Slot " + overIndex + " is empty");
            for (int i = overIndex; i > 0; i--)
            {
                print("Loop");
                if (_inventorySlotArray[i].Item == DragItem) break;
                if (_inventorySlotArray[i].Item && !_inventorySlotArray[i + 1].Item)
                {
                    print("Find first empty slot = " + (i + 1));
                    _inventorySlotArray[i + 1].Item = DragItem;
                    DragItemLeft(dragItemIndex, i + 1);
                    break;
                }
            }
            print("Done !");
            // for (int i = 0; i < _inventorySlotArray.Length; i++)
            // {
            //     if (!_inventorySlotArray[i].Item && _inventorySlotArray[i] != slotOver)
            //     {
            //         print("Over Empty slot = set to first empty");
            //         _inventorySlotArray[dragItemIndex].Item = null;
            //         _inventorySlotArray[i].Item = DragItem;
            //         DragItemLeft(dragItemIndex, i);
            //         return;
            //     }
            // }
        }
    }

    private int GetDragItemIndex()
    {
        int dragItemIndex = 0;
        for (int i = 0; i < _inventorySlotArray.Length; i++)
        {
            if (_inventorySlotArray[i].Item != DragItem) continue;
            dragItemIndex = i;
            break;
        }

        return dragItemIndex;
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
