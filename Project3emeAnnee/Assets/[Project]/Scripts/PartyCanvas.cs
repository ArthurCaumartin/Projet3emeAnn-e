using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lifeText;
    [SerializeField] private Image _lifeImage;

    public void UpdateLifeBar(int max, int current)
    {
        _lifeText.text = current + "/" + max;
        _lifeImage.fillAmount = Mathf.InverseLerp(0, max, current);
    }
}
