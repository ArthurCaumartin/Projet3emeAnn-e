using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public Transform ItemContainer { get => _inventoryItemContainer; }
    [SerializeField] private GameObject _inventorySlotPrefab;
    private List<InventoryItem> _inventoryItemList = new List<InventoryItem>();
    private InventorySlot[] _mainInventorySlotArray;

    [Header("To Move away :")]
    [SerializeField] private GameObject _itemPrefab;
    private InventoryItem _dragItem;
    //! Drag is set by InventoryItem while drag
    public InventoryItem DragItem
    {
        get => _dragItem;
        set
        {
            //! Disable ray target to enable slot inventory pointer event through inventory item
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
        InstantiateInventory();
    }

    private void Update()
    {
        if (_dragItem)
        {
            _dragItem.transform.position = Input.mousePosition;
        }
    }

    public void InstantiateInventory()
    {
        DeleteInventory();
        _mainInventorySlotArray = new InventorySlot[_inventorySize];
        for (int i = 0; i < _mainInventorySlotArray.Length; i++)
        {
            GameObject newSlot = Instantiate(_inventorySlotPrefab, transform.position, Quaternion.identity, _inventorySlotContainer);
            newSlot.name = "Slot_" + i;
            _mainInventorySlotArray[i] = newSlot.GetComponent<InventorySlot>().Initialize(this);
        }
    }

    private void DeleteInventory()
    {
        if (_mainInventorySlotArray != null && _mainInventorySlotArray.Length > 0)
        {
            for (int i = 0; i < _mainInventorySlotArray.Length; i++)
                DestroyImmediate(_mainInventorySlotArray[i]);
        }
    }

    [ContextMenu("Spawn New Item")]
    public void SpawnNewInventoryItem()
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

    //! Add Item on the first inventory slot and push other item
    public void AddItemToInventory(InventoryItem item)
    {
        DragItemRight(_mainInventorySlotArray.Length - 1, 0);
        _mainInventorySlotArray[0].SetItemInSlot(item);
        _inventoryItemList.Add(item);
    }

    private void DragItemRight(int startIndex, int endIndex)
    {
        //! drag index in inventory from start to end index
        //! Trash item if the item goes out of the mainInventory
        for (int i = startIndex; i > endIndex; i--)
        {
            if (i == _mainInventorySlotArray.Length - 1 && _mainInventorySlotArray[i].Item)
            {
                TrashItem(_mainInventorySlotArray[i].Item);
            }
            _mainInventorySlotArray[i].SetItemInSlot(_mainInventorySlotArray[i - 1].Item);
        }
    }

    private void DragItemLeft(int start, int end)
    {
        //! drag index in inventory from start to end index
        for (int i = start; i < end; i++)
        {
            _mainInventorySlotArray[i].SetItemInSlot(_mainInventorySlotArray[i + 1].Item);
            if (i == end - 1)
            {
                _mainInventorySlotArray[i + 1].SetItemInSlot(null);
            }
        }
    }

    private void RemoveItem(int itemIndex, bool dragToLeft = false)
    {
        print("Remove Item, Index : " + itemIndex);
        _mainInventorySlotArray[itemIndex].SetItemInSlot(null);
        //! Drag to left to fill the holl
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
                DragItem.LastSlot.SetItemInSlot(null);
            else
                RemoveItem(dragItemIndex, true);

            slotOver.SetItemInSlot(DragItem);
            slotOver.OnPutInNonMainSlot();
            return;
        }

        if (dragItemIndex == -1)
        {
            //! If drag come from non main slot, clear last slot
            if(IsAllInventorySlotFill()) return;
            DragItem.LastSlot.SetItemInSlot(null);
            if (slotOver.Item)
            {
                DragItemRight(GetFirstEmptySlotIndex(), overIndex);
                slotOver.SetItemInSlot(DragItem);
            }
            else
            {
                _mainInventorySlotArray[GetFirstEmptySlotIndex()].SetItemInSlot(DragItem);
            }
        }
        else
        {
            //! If drag item come from main slot
            if (slotOver.Item)
            {
                // print("Item In Slot");
                if (dragItemIndex < overIndex) DragItemLeft(dragItemIndex, overIndex);
                if (dragItemIndex > overIndex) DragItemRight(dragItemIndex, overIndex);
                slotOver.SetItemInSlot(DragItem);
            }
            else
            {
                // print("No Item in over slot");
                int indexToFind = GetFirstEmptySlotIndex();
                _mainInventorySlotArray[indexToFind].SetItemInSlot(DragItem);
                DragItemLeft(dragItemIndex, indexToFind);
            }
        }
    }

    private int GetFirstEmptySlotIndex()
    {
        //! Travel backward in the inventory to find the slot faster
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

    public int GetSlotIndex(InventorySlot slot)
    {
        for (int i = 0; i < _mainInventorySlotArray.Length; i++)
        {
            if (_mainInventorySlotArray[i] == slot) return i;
        }
        //! Return -1 if the DragItem isn't in the main Inventory
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
        //! Return -1 if the DragItem isn't in the main Inventory
        return dragItemIndex;
    }

    public bool IsAllInventorySlotFill()
    {
        foreach (var item in _mainInventorySlotArray)
        {
            if(!item.Item)
                return false;
        }
        return true;
    }
}