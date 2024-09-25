using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Inventory _inventory;
    private Image _image;
    private RectTransform _rectTransform;
    private InventorySlot _slotToGoOnPointerUp;

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
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_slotToGoOnPointerUp)
        {
            _slotToGoOnPointerUp.Item = null;
        }
        _inventory.DragItem = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_slotToGoOnPointerUp)
            _inventory.ItemGrabOverSlot(_slotToGoOnPointerUp);

        _inventory.DragItem = null;
    }

    public void EnableSetOnMouseRealse(InventorySlot value)
    {
        _slotToGoOnPointerUp = value;
    }
}