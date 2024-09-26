using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //! ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //! ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //! ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //TODO refaire l'interaction de l'inventaire avec le raycaster pour eviter les conflit entre les PointerEvent !
    //! ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //! ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //! ////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
        // print("Go to Right");
        for (int i = start; i > end; i--)
        {
            if (i == _mainInventorySlotArray.Length - 1 && _mainInventorySlotArray[i].Item)
            {
                TrashItem(_mainInventorySlotArray[i].Item);
            }
            _mainInventorySlotArray[i].SetItem(_mainInventorySlotArray[i - 1].Item);
        }
    }

    private void DragItemLeft(int fromIndex, int toIndex)
    {
        for (int i = fromIndex; i < toIndex; i++)
        {
            _mainInventorySlotArray[i].SetItem(_mainInventorySlotArray[i + 1].Item);
            if (i == toIndex - 1)
            {
                _mainInventorySlotArray[i + 1].SetItem(null);
            }
        }
    }

    private void RemoveItem(InventoryItem itemToRemove, bool dragToLeft = false)
    {
        int dargIndex = GetDragItemIndex();
        _mainInventorySlotArray[dargIndex].SetItem(null);
        if (dragToLeft) DragItemLeft(dargIndex, _mainInventorySlotArray.Length - 1);
    }

    public void AddGrabItenToInventory(InventorySlot slotOver)
    {
        if (!DragItem) return;
        if (slotOver.Item == DragItem) return;

        //! If drag come from non main slot, clear last slot
        if (!_mainInventorySlotArray.Contains(DragItem.lastSlot))
            DragItem.lastSlot.SetItem(null);

        //! If drag goes in non main slot
        if (!_mainInventorySlotArray.Contains(slotOver))
        {
            print("Add Item to other slot");
            if (slotOver.Item) TrashItem(slotOver.Item);
            RemoveItem(DragItem, true);
            slotOver.SetItem(DragItem);
            return;
        }

        int dragItemIndex = GetDragItemIndex();
        print("Drag index : " + dragItemIndex);
        int overIndex = Array.IndexOf(_mainInventorySlotArray, slotOver);

        if (slotOver.Item)
        {
            print("Item In Slot");
            if (dragItemIndex < overIndex) DragItemLeft(dragItemIndex, overIndex);
            if (dragItemIndex > overIndex) DragItemRight(dragItemIndex, overIndex);
            slotOver.SetItem(DragItem);
            return;
        }

        //! If no item in slot
        print("No Item in over slot");
        for (int i = overIndex; i > 0; i--)
        {
            if (_mainInventorySlotArray[i].Item == DragItem) break;
            if (_mainInventorySlotArray[i].Item && !_mainInventorySlotArray[i + 1].Item)
            {
                _mainInventorySlotArray[i + 1].SetItem(DragItem);
                DragItemLeft(dragItemIndex, i + 1);
                break;
            }
        }
    }

    private int GetDragItemIndex()
    {
        int dragItemIndex = 0;
        for (int i = 0; i < _mainInventorySlotArray.Length; i++)
        {
            if (_mainInventorySlotArray[i].Item != DragItem) continue;
            dragItemIndex = i;
            break;
        }

        return dragItemIndex;
    }
}