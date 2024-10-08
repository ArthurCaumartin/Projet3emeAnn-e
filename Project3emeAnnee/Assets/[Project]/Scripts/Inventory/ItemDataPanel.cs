using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _partDescription;
    [SerializeField] private Image _partImage;
    [SerializeField] private TextMeshProUGUI _partDetail;

    [Header("Text Stat Reference :")]
    [SerializeField] private TextMeshProUGUI _textDamage;
    [SerializeField] private TextMeshProUGUI _textAttackPerSecond;
    [SerializeField] private TextMeshProUGUI _textRange;
    [SerializeField] private TextMeshProUGUI _textRotateSpeed;
    [SerializeField] private TextMeshProUGUI _textProjectileSpeed;
    [SerializeField] private TextMeshProUGUI _textPerforationCount;

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
            //TODO Get stat multiplier
        }

        if (part is ScriptableBody)
        {
            StatContainer stat = ((ScriptableBody)part).stat;
            _partDetail.text = 
            "Damage : " + stat.damage.ToString() + "\n" +
            "AttackSpeed : " + stat.attackPerSecond.ToString() + "\n" +
            "Range : " + stat.range.ToString() + "\n" +
            "Projectile Speed : " + stat.projectileSpeed.ToString() + "\n" +
            "Perforation Count : " + stat.perforationCount.ToString() + "\n";
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