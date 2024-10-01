using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private InventoryItem _inventoryItem;
    [SerializeField] private PartType _partToTake;
    [SerializeField] TurretPanel _turretPanel;
    [SerializeField] ScriptableTurretPart _part;
    public InventoryItem Item { get => _inventoryItem; }
    private Inventory _inventory;
    private Image _image;

    private void Start()
    {
        //! Get inventory ref if Initialize wasn't call
        if (!_inventory) _inventory = Inventory.instance;
        name = name + "_" + _partToTake.ToString();
        _image = GetComponentInChildren<Image>();
        float r = Random.value;
        if (r < .25f) _image.transform.eulerAngles = new Vector3(0, 0, 0);
        if (r > .25f && r < .5f) _image.transform.eulerAngles = new Vector3(0, 0, 90);
        if (r > .5f && r < .75f) _image.transform.eulerAngles = new Vector3(0, 0, 180);
        if (r > .75f) _image.transform.eulerAngles = new Vector3(0, 0, 270);
    }

    public InventorySlot Initialize(Inventory inventory)
    {
        _inventory = inventory;
        return this;
    }

    //! Fonction call a lot of time with item = null
    public void SetItemInSlot(InventoryItem item)
    {
        _inventoryItem = item;

        if (_inventoryItem)
        {
            item.transform.SetParent(_inventory.GetSlotIndex(this) == -1 ? transform : _inventory.ItemContainer);
            _inventoryItem.LastSlot = this;
        }
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
        if (!_inventory.DragItem) return;
        ScriptableTurretPart part = _inventory.DragItem.GetTurretPartOnDescriptor();
        if (_partToTake == part.partType || _partToTake == PartType.None)
            _inventory.AddGrabItenToInventory(this);
    }

    public void OnPutInNonMainSlot()
    {
        if (_turretPanel) _turretPanel.ChangeTurretPart(Item.GetTurretPartOnDescriptor());
    }
}


