using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Inventory _inventory;
    private Image _image;
    private RectTransform _rectTransform;
    public InventorySlot lastSlot;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.color = Color.HSVToRGB(Random.value, .7f, .7f);
        _rectTransform = (RectTransform)transform;
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

    public void SetRaycastTarget(bool value) { _image.raycastTarget = value; }
    // public void OnPointerDown(PointerEventData eventData) { _inventory.DragItem = this; }
    // public void OnPointerUp(PointerEventData eventData) { _inventory.DragItem = null; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _inventory.DragItem = this;
        print("Start Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _inventory.DragItem = null;

        // InventorySlot slot = RaycasterUI.instance.GetTypeUnderMouse<InventorySlot>();
        // slot.AddGrabItemToSlot();
        // print("End Drag");
    }
}