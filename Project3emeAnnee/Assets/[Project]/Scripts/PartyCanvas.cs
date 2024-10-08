using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gazCountText;
    [Space]
    [SerializeField] private TextMeshProUGUI _lifeText;
    [SerializeField] private Image _lifeImage;
    [Space]
    [SerializeField] private TextMeshProUGUI _partyStateText;


    public void UpdateLifeBar(int max, int current)
    {
        _lifeText.text = current + "/" + max;
        _lifeImage.fillAmount = Mathf.InverseLerp(0, max, current);
    }

    public void UpdateGazCount(float gazCount)
    {
        _gazCountText.text = "Gaz Harvest : " + ((int)gazCount).ToString();
    }

    public void UpdatePartyState(PartyState state)
    {
        _partyStateText.text = state.ToString();
    }
}
