using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestHighlight : Selectable
{
    public GameObject _infoPopup;
    private bool isHighlighted;
    void Update()
    {
        if (IsHighlighted() && !isHighlighted)
        {
            _infoPopup.SetActive(true);
            isHighlighted = true;
        }
        else if(!IsHighlighted() && isHighlighted)
        {
            _infoPopup.SetActive(false);
            isHighlighted = false;
        }
    }
}
