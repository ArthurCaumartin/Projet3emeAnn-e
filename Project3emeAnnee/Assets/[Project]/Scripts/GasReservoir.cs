using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GasReservoir : MonoBehaviour
{
    [SerializeField] private int _essenceQuantity = 500;
    [SerializeField] private RectTransform _startButton;

    private void Start()
    {
        GetComponent<TargetDetector>().TriggerEnvent.AddListener(SetStartButtonScale);
        _startButton.GetComponent<Button>().onClick.AddListener(() => GameManager.instance.SetGameState(GameState.TowerDefence));
        SetStartButtonScale(false);

    }

    public void SetStartButtonScale(bool value)
    {
        _startButton.DOScale(value ? Vector3.one : Vector3.zero, .1f)
        .SetEase(Ease.Linear);
    }
}
