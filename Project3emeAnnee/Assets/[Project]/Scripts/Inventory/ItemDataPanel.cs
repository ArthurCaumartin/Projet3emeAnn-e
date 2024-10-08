using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _partDescription;
    [SerializeField] private Image _partImage;
    [SerializeField] private TextMeshProUGUI _partDetail;

    public void SetDataToShow(ScriptableTurretPart part)
    {
        if (!part)
        {
            print(name + " : part is null can't show data :/");
        }

        gameObject.SetActive(true);

        _itemName.text = part.partName;
        _partImage.sprite = part.inventorySprite;
        _partDescription.text = part.description;

        if (part is ScriptableCannon)
        {
            _partDetail.text = "More Data for canon comming soon :)";
        }

        if (part is ScriptableBody)
        {
            _partDetail.text = "Promis je fait les stats aprés :)";
        }
    }

    public void ClearDataToShow()
    {
        gameObject.SetActive(false);

        _itemName.text = null;
        _partDescription.text = null;
        _partImage.sprite = null;
        _partDetail.text = null;
    }
}