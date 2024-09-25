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

    public bool DetecteMouseRelease(GameObject toFind)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(pointerEventData, results);
        if(results.Count == 0) return false;

        foreach (var item in results)
        {
            if(item.gameObject == toFind)
                return true;
        }
        return false;
    }
}