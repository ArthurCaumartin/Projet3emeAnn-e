using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPartDescritor : MonoBehaviour
{
    [SerializeField] private ScriptableTurretPart _turetPart;
    private Image _image;

    public ScriptableTurretPart TurretPart
    {
        get => _turetPart;

        set
        {
            _turetPart = value;
            _image = GetComponent<Image>();
            _image.sprite = _turetPart.inventorySprite;
            _image.color = _turetPart.color;
        }
    }
}
