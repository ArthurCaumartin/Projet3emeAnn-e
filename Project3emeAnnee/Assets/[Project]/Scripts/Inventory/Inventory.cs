using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private int _inventorySize = 12;
    [SerializeField] private float _itemAnimationSpeed = 5;
    public float AnimationSpeed { get => _itemAnimationSpeed; }
    [Header("UI Reference :")]
    [SerializeField] private Transform _inventorySlotContainer;
    [SerializeField] private Transform _inventoryItemContainer;
    [SerializeField] private GameObject _inventorySlotPrefab;
    private List<InventoryItem> _inventoryItemList = new List<InventoryItem>();
    private InventorySlot[] _mainInventorySlotArray;

    [Header("To Move away :")]
    [SerializeField] private GameObject _itemPrefab;
    private InventoryItem _dragItem;
    public InventoryItem DragItem
    {
        get => _dragItem;
        set
        {
            //! Disable ray target to enable slot inventory pointer event through inventory slot
            foreach (var item in _inventoryItemList)
                item.SetRaycastTarget(!value);
            _dragItem = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DrawInventory();
    }

    private void Update()
    {
        if (_dragItem)
        {
            _dragItem.transform.position = Input.mousePosition;
        }
    }

    public void DrawInventory()
    {
        if (_mainInventorySlotArray != null && _mainInventorySlotArray.Length > 0)
        {
            // print("clear inventory slot");
            for (int i = 0; i < _mainInventorySlotArray.Length; i++)
                DestroyImmediate(_mainInventorySlotArray[i]);
        }

        _mainInventorySlotArray = new InventorySlot[_inventorySize];
        for (int i = 0; i < _mainInventorySlotArray.Length; i++)
        {
            GameObject newSlot = Instantiate(_inventorySlotPrefab, transform.position, Quaternion.identity, _inventorySlotContainer);
            newSlot.name = "Slot_" + i;
            _mainInventorySlotArray[i] = newSlot.GetComponent<InventorySlot>().Initialize(this);
        }
    }

    [ContextMenu("Spawn New Item")]
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
        // print("Trash");
        if (itemToTrash == DragItem) return;
        _inventoryItemList.Remove(itemToTrash);
        itemToTrash.TrashAnimation();
    }

    public void AddItemToInventory(InventoryItem item)
    {
        DragItemRight(_mainInventorySlotArray.Length - 1, 0);
        _mainInventorySlotArray[0].SetItem(item);
        _inventoryItemList.Add(item);
    }

    private void DragItemRight(int start, int end)
    {
        for (int i = start; i > end; i--)
        {
            if (i == _mainInventorySlotArray.Length - 1 && _mainInventorySlotArray[i].Item)
            {
                TrashItem(_mainInventorySlotArray[i].Item);
            }
            _mainInventorySlotArray[i].SetItem(_mainInventorySlotArray[i - 1].Item);
        }
    }

    private void DragItemLeft(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            _mainInventorySlotArray[i].SetItem(_mainInventorySlotArray[i + 1].Item);
            if (i == end - 1)
            {
                _mainInventorySlotArray[i + 1].SetItem(null);
            }
        }
    }

    private void RemoveItem(int itemIndex, bool dragToLeft = false)
    {
        print("Remove Item, Index : " + itemIndex);
        _mainInventorySlotArray[itemIndex].SetItem(null);
        if (dragToLeft) DragItemLeft(itemIndex, _mainInventorySlotArray.Length - 1);
    }

    public void AddGrabItenToInventory(InventorySlot slotOver)
    {
        if (!DragItem) return;
        if (slotOver.Item == DragItem) return;

        int dragItemIndex = GetDragItemIndex();
        int overIndex = GetSlotIndex(slotOver);

        //! If drag goes in non main slot
        if (!_mainInventorySlotArray.Contains(slotOver))
        {
            print("Add Item to other slot");
            if (slotOver.Item) TrashItem(slotOver.Item);

            if (dragItemIndex == -1)
                DragItem.lastSlot.SetItem(null);
            else
                RemoveItem(dragItemIndex, true);

            slotOver.SetItem(DragItem);
            return;
        }

        if (dragItemIndex == -1)
        {
            //! If drag come from non main slot, clear last slot
            DragItem.lastSlot.SetItem(null);
            if (slotOver.Item)
            {
                DragItemRight(GetFirstEmptySlotIndex(), overIndex);
                slotOver.SetItem(DragItem);
            }
            else
            {
                _mainInventorySlotArray[GetFirstEmptySlotIndex()].SetItem(DragItem);
            }
        }
        else
        {
            if (slotOver.Item)
            {
                // print("Item In Slot");
                if (dragItemIndex < overIndex) DragItemLeft(dragItemIndex, overIndex);
                if (dragItemIndex > overIndex) DragItemRight(dragItemIndex, overIndex);
                slotOver.SetItem(DragItem);
            }
            else
            {
                // print("No Item in over slot");
                int indexToFind = GetFirstEmptySlotIndex();
                _mainInventorySlotArray[indexToFind].SetItem(DragItem);
                DragItemLeft(dragItemIndex, indexToFind);
            }
        }
    }

    private int GetFirstEmptySlotIndex()
    {
        for (int i = _mainInventorySlotArray.Length - 1; i > 0; i--)
        {
            // if (_mainInventorySlotArray[i].Item == DragItem) break;
            if (_mainInventorySlotArray[i].Item && !_mainInventorySlotArray[i + 1].Item)
            {
                return i + 1;
            }
        }
        return -1;
    }

    private int GetSlotIndex(InventorySlot slot)
    {
        for (int i = 0; i < _mainInventorySlotArray.Length; i++)
        {
            if (_mainInventorySlotArray[i] == slot) return i;
        }
        return -1;
    }

    private int GetDragItemIndex()
    {
        int dragItemIndex = -1;
        for (int i = 0; i < _mainInventorySlotArray.Length; i++)
        {
            if (_mainInventorySlotArray[i].Item == DragItem)
            {
                dragItemIndex = i;
                return dragItemIndex;
            }
        }
        return dragItemIndex;
    }
}


// public static class UUU
// {
//     public static int GetIndex<T>(this T[] array, object toFind)
//     {
//         if(toFind is not T) return -1;

//         for (int i = 0; i < array.Length; i++)
//         {
//             if(array[i].Equals(toFind)) return i;
//         }
//         return -1;
//     }
// }