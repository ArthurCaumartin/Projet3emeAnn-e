using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycasterUI : MonoBehaviour
{
    public static RaycasterUI instance;
    [SerializeField] private GraphicRaycaster _graphicRaycaster;

    private void Awake()
    {
        instance = this;
    }

    public typeToFind GetTypeUnderMouse<typeToFind>() where typeToFind : MonoBehaviour
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(pointerEventData, results);
        if(results.Count == 0) return null;

        foreach (var item in results)
        {
            typeToFind typeInstance = item.gameObject.GetComponent<typeToFind>();
            if(typeInstance && typeInstance is typeToFind)
            {
                return typeInstance;
            }
        }
        return null;
    }   
}