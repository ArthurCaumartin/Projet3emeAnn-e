using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Inventory _inventory;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.color = Color.HSVToRGB(Random.value, .7f, .7f);
    }

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _inventory.DragItem = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inventory.DragItem = null;
    }

    public void SetRaycastTarget(bool value)
    {
        _image.raycastTarget = value;
    }
}