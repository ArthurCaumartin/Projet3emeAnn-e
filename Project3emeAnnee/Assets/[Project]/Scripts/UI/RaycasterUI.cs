using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycasterUI : MonoBehaviour
{
    public static RaycasterUI instance;
    [SerializeField] private GraphicRaycaster[] _graphicRayArray;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _graphicRayArray = new GraphicRaycaster[0];
    }

    public T GetTypeUnderMouse<T>() where T : MonoBehaviour
    {
        //TODO BUG : tout les GraficRaycaster sont pas reccupe ? donc toute les UI sont pas capté
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        if (_graphicRayArray.Length == 0) _graphicRayArray = Resources.FindObjectsOfTypeAll(typeof(GraphicRaycaster)) as GraphicRaycaster[];
        if (_graphicRayArray.Length == 0) return null;
        for (int i = 0; i < _graphicRayArray.Length; i++)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            _graphicRayArray[i].Raycast(pointerEventData, results);
            if (results.Count == 0) return null;

            foreach (var item in results)
            {
                T instance = item.gameObject.GetComponent<T>();
                if (instance && instance is T) return instance;
            }
        }
        return null;
    }
}