using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoreSell : MonoBehaviour, IDropHandler
{
    private ResearchManager _researchManager;

    private void Start()
    {
        _researchManager = GetComponentInParent<ResearchManager>();
    }

    // If a Core Sprite that we Drag&Drop is dropped on this object, sell it and destroy the Core Sprite
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DragDropResearch objectScript = eventData.pointerDrag.GetComponent<DragDropResearch>();
            _researchManager.SellCore(objectScript._upgradeValue);
            
            DestroyImmediate(eventData.pointerDrag);
        }
    }
}
