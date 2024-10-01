using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoreTrash : MonoBehaviour, IDropHandler
{
    private ResearchManager _researchManager;

    private void Start()
    {
        _researchManager = GetComponentInParent<ResearchManager>();
    }

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
