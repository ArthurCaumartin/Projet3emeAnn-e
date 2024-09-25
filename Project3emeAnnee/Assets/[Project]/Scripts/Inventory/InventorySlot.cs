using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class InventorySlot : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private InventoryItem _inventoryItem;
    public InventoryItem Item { get => _inventoryItem; set => _inventoryItem = value; }
    private Inventory _inventory;
    private bool _isMouseOver;
    private float _mouseOverTime;
    private bool _isFromMainIventory = false;
    private bool _lastFrameRightClic = false;

    private void Start()
    {
        if (!_inventory) _inventory = Inventory.instance;
    }

    public InventorySlot Initialize(Inventory inventory)
    {
        _isFromMainIventory = true;
        _inventory = inventory;
        return this;
    }

    public void UpdateItemPos(float speed)
    {
        if (!_inventoryItem || _inventory.DragItem == _inventoryItem) return;
        _inventoryItem.transform.position = Vector3.Lerp(_inventoryItem.transform.position, transform.position, Time.deltaTime * speed);
    }

    private void Update()
    {
        UpdateItemPos(_inventory.AnimationSpeed);

        if (!Input.GetMouseButton(0) && _inventory.DragItem)
        {
            print("YAAAAAAAAAAAAAAAAA");
        }
        _lastFrameRightClic = Input.GetMouseButton(0);

        if (_inventory.DragItem && _inventoryItem == _inventory.DragItem) return;
        _mouseOverTime = _isMouseOver ? _mouseOverTime + Time.deltaTime : 0;
        if (_isFromMainIventory && _mouseOverTime > .001f)
        {
            _inventory.ItemGrabOverSlot(this);
            _mouseOverTime = 0;
        }
    }

    private void OnMouseUp()
    {
        if (!_isFromMainIventory) return;
        if (_mouseOverTime > .001f)
        {
            _inventory.ItemGrabOverSlot(this);
            _mouseOverTime = 0;
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        _isMouseOver = true;

        if (_inventory.DragItem && !_isFromMainIventory && _inventory.DragItem)
        {
            _inventory.DragItem.EnableSetOnMouseRealse(this);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _isMouseOver = false;

        if (_inventory.DragItem) _inventory.DragItem.EnableSetOnMouseRealse(null);
    }
}
