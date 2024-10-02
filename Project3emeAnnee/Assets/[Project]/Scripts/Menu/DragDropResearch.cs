using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropResearch : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private UpgradeSlot _actualSlot;

    public int _upgradeValue;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventDate)
    {
        // print("On Point Down");
    }

    
    //Start Dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnLeaveSlot();
        
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.6f;
    }

    //End Dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    // Save the slot where this Core Sprite is dropped
    public void OnDropOnSlot(UpgradeSlot slot)
    {
        _actualSlot = slot;
    }

    // Call the slot where this Core Sprite were and reset the infos
    public void OnLeaveSlot()
    {
        if (_actualSlot == null) return;
        
        _actualSlot.OnLeaveSlot();
        
        _actualSlot = null;
    }
}
