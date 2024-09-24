using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    private RectTransform _rectTransform;
    private bool _isGrab;
    public bool IsGrab { get => _isGrab; }

    private void Start()
    {
        _rectTransform = (RectTransform)transform;
        GetComponent<Image>().color = Color.HSVToRGB(Random.value, .7f, .7f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isGrab = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!_isGrab) return;
        _rectTransform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isGrab = false;
    }
}