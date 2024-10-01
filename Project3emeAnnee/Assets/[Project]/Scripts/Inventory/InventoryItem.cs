using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Inventory _inventory;
    private Image _image;
    private RectTransform _rectTransform;
    private InventorySlot lastSlot;
    public InventorySlot LastSlot { get => lastSlot; set => lastSlot = value; }
    private TurretPartDescritor _turretPartDescriptor;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.color = Color.HSVToRGB(Random.value, .7f, .7f);
        _rectTransform = (RectTransform)transform;
        _turretPartDescriptor = GetComponent<TurretPartDescritor>();
    }

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void TrashAnimation()
    {
        Vector3 startPos = _rectTransform.position;
        _rectTransform.DOMove(startPos + new Vector3(0, -_rectTransform.rect.height * 3, 0), .2f)
        .SetEase(Ease.Linear)
        .OnComplete(() => Destroy(gameObject));
    }

    //! to let the pointer call Event througt InventoryItem
    public void SetRaycastTarget(bool value) { _image.raycastTarget = value; }

    public void OnBeginDrag(PointerEventData eventData) { _inventory.DragItem = this; }
    public void OnEndDrag(PointerEventData eventData) { _inventory.DragItem = null; }
    public void OnDrag(PointerEventData eventData) { }

    public ScriptableTurretPart GetTurretPartOnDescriptor()
    {
        return _turretPartDescriptor.GetTurretPart();
    }
}