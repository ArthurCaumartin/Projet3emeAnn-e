using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GasReservoir : MonoBehaviour
{
    [SerializeField] private int _gasQuantity = 500;
    [SerializeField] private RectTransform _startButton;
    [SerializeField] private Button _launchWaveButton;

    private void Start()
    {
        GetComponent<TargetDetector>().TriggerEnvent.AddListener(SetStartButtonScale);
        _startButton.GetComponent<Button>().onClick.AddListener(StartTowerDefencePlacement);
        _launchWaveButton.onClick.AddListener(StartTowerDefence);
        _launchWaveButton.gameObject.SetActive(false);
        SetStartButtonScale(false);
    }

    public void StartTowerDefencePlacement()
    {
        PartyManager.instance.StartTowerDefencePlacement(transform);
        _launchWaveButton.gameObject.SetActive(true);
    }

    public void StartTowerDefence()
    {
        PartyManager.instance.StartTowerDefence();
        _launchWaveButton.gameObject.SetActive(false);
    }

    public void SetStartButtonScale(bool value)
    {
        _startButton.DOScale(value ? Vector3.one : Vector3.zero, .1f)
        .SetEase(Ease.Linear);
    }
}
