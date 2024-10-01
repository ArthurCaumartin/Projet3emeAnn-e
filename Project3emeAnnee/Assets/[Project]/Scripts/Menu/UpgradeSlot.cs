using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour, IDropHandler
{
    public GameObject _objectDropped;
    private RectTransform _objectDroppedTransform;
    private Image _upgradeSlotImage;

    private void Start()
    {
        _upgradeSlotImage = GetComponent<Image>();
    }
    
    // If an object is dropped on this object, change color, save the object, place it on this object, call the object to register this slot onto it
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && _objectDropped == null)
        {
            _upgradeSlotImage.color = Color.red;
            
            _objectDropped = eventData.pointerDrag;
            
            _objectDroppedTransform = _objectDropped.GetComponent<RectTransform>();
            DragDropResearch _objectScript = _objectDropped.GetComponent<DragDropResearch>();
            _objectScript.OnDropOnSlot(this);
            
            _objectDroppedTransform.position = GetComponent<RectTransform>().position;
            
        }
    }

    // Reset the slot if the object leave
    public void OnLeaveSlot()
    {
        _objectDropped = null;
        _upgradeSlotImage.color = Color.black;
    }
    
    // If all slots are reset, call this
    public void ResetSlot()
    {
        if (_objectDropped == null) return;
        _objectDroppedTransform.anchoredPosition = Vector2.zero;
        _objectDropped = null;
        
        _upgradeSlotImage.color = Color.black;
    }
}
