using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private InventoryItem _inventoryItem;
    [SerializeField] private PartType _partToTake;
    [SerializeField] TurretSetter _turretSetter;
    [SerializeField] ScriptableTurretPart _part;
    public InventoryItem Item { get => _inventoryItem; }
    private Inventory _inventory;

    private void Start()
    {
        //! Get inventory ref if Initialize wasn't call
        if (!_inventory) _inventory = Inventory.instance;
        _turretSetter = transform.parent.GetComponent<TurretSetter>();
    }

    public InventorySlot Initialize(Inventory inventory)
    {
        _inventory = inventory;
        return this;
    }

    public void SetItemInSlot(InventoryItem item)
    {
        _inventoryItem = item;
        if (_inventoryItem) _inventoryItem.LastSlot = this;
    }

    private void MoveItemToSlot(float speed)
    {
        if (!_inventoryItem || _inventory.DragItem == _inventoryItem) return;
        _inventoryItem.transform.position = Vector3.Lerp(_inventoryItem.transform.position, transform.position, Time.deltaTime * speed);
    }

    private void Update()
    {
        MoveItemToSlot(_inventory.AnimationSpeed);
    }

    //! Reorganise inventory on mouse over
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!_inventory.DragItem) return;
        ScriptableTurretPart part = _inventory.DragItem.GetTurretPartOnDescriptor();
        if (_partToTake == part.partType || _partToTake == PartType.None)
            _inventory.AddGrabItenToInventory(this);
    }

    public void OnPutInNonMainSlot()
    {
        if (_turretSetter) _turretSetter.ChangeTurretPart(Item.GetTurretPartOnDescriptor());
    }
}


