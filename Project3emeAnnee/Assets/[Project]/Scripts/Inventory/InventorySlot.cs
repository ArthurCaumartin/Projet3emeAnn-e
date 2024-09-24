using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private InventoryItem _inventoryItem;
    public InventoryItem Item { get => _inventoryItem; set => _inventoryItem = value; }

    public InventorySlot Initialize(Inventory inventory)
    {
        return this;
    }

    public void UpdateItemPos(float speed)
    {
        if(!_inventoryItem) return;
        _inventoryItem.transform.position = Vector3.Lerp(_inventoryItem.transform.position, transform.position, Time.deltaTime * speed);
    }
}
