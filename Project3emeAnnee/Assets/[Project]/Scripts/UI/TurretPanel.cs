using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TurretPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform _panelRect;
    [SerializeField] private float _showAnimationSpeed = 5;

    private void Start()
    {
        ShowPanel(true, true);
    }

    private void ShowPanel(bool open, bool isInstante = false)
    {
        Vector3 startScale = _panelRect.localScale;
        Vector3 targetScale = open ? Vector3.one : Vector3.zero;

        if (isInstante)
        {
            _panelRect.localScale = targetScale;
            return;
        }

        DOTween.To((time) =>
        {
            _panelRect.localScale = Vector3.Lerp(startScale, targetScale, time);
        }, 0, 1, _showAnimationSpeed)
        .SetSpeedBased()
        .SetEase(Ease.Linear);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // print("Enter : " + name + " / " + transform.localScale);
        // ShowPanel(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // print("Exit : " + name + " / " + transform.localScale);
        // ShowPanel(false);
    }
}
