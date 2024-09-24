using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private InventoryItem _inventoryItem;
    public InventoryItem Item { get => _inventoryItem; set => _inventoryItem = value; }
    private Inventory _inventory;
    private bool _isMouseOver;
    private float _mouseOverTime;

    public InventorySlot Initialize(Inventory inventory)
    {
        _inventory = inventory;
        return this;
    }

    public void UpdateItemPos(float speed)
    {
        if (!_inventoryItem || _inventory.DragItem == _inventory) return;
        _inventoryItem.transform.position = Vector3.Lerp(_inventoryItem.transform.position, transform.position, Time.deltaTime * speed);
    }

    private void Update()
    {
        if(_inventory.DragItem && _inventoryItem == _inventory.DragItem) return;
        _mouseOverTime = _isMouseOver ? _mouseOverTime + Time.deltaTime : 0;
        if(_mouseOverTime > 1) print("Drag item over slot : " + name);
    }

    public void OnPointerMove(PointerEventData eventData) { _isMouseOver = true; }

    public void OnPointerExit(PointerEventData eventData) { _isMouseOver = false; }
}