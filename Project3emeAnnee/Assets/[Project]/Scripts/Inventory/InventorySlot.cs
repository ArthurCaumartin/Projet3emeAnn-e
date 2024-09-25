using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private InventoryItem _inventoryItem;
    public InventoryItem Item { get => _inventoryItem; }


    private Inventory _inventory;

    private void Start()
    {
        if (!_inventory) _inventory = Inventory.instance;
    }

    public InventorySlot Initialize(Inventory inventory)
    {
        _inventory = inventory;
        return this;
    }

    public void SetItem(InventoryItem item)
    {
        _inventoryItem = item;
        if(_inventoryItem) _inventoryItem.lastSlot = this;
    }

    private void UpdateItemPos(float speed)
    {
        if (!_inventoryItem || _inventory.DragItem == _inventoryItem) return;
        _inventoryItem.transform.position = Vector3.Lerp(_inventoryItem.transform.position, transform.position, Time.deltaTime * speed);
    }

    private void Update()
    {
        UpdateItemPos(_inventory.AnimationSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _inventory.AddGrabItenToInventory(this);
    }
}


