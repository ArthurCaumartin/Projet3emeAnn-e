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

    public T GetTypeUnderMouse<T>() where T : MonoBehaviour
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(pointerEventData, results);
        if (results.Count == 0) return null;

        foreach (var item in results)
        {
            T instance = item.gameObject.GetComponent<T>();
            if (instance && instance is T) return instance;
        }
        return null;
    }
}