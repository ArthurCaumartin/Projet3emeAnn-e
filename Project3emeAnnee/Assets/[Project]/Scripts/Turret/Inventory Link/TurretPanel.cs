using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretPanel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] private TurretBaker _turretBaker;
    [SerializeField] private RectTransform _panelContainer;
    [SerializeField] private RectTransform _placementButton;
    private TurretManager _turretManager;
    public TurretBaker TurretBaker { get => _turretBaker; set => _turretBaker = value; }

    private void Start()
    {
        _panelContainer.gameObject.SetActive(false);
        _placementButton.GetComponent<Button>().onClick.AddListener(() => _turretManager.ClicOnPlacementButton(this));
    }

    public TurretPanel Initialize(TurretManager manager, TurretBaker baker)
    {
        _turretManager = manager;
        _turretBaker = baker;
        return this;
    }

    public void ChangeTurretPart(ScriptableTurretPart part)
    {
        _turretBaker.SetTurretComponent(part);
    }

    public void OpenPanel(bool value)
    {
        _panelContainer.gameObject.SetActive(value);
    }

    public void ShowPlacementButton(bool isShow)
    {
        // print(isShow);
        _placementButton.DOAnchorPos(isShow ? new Vector2(-_placementButton.rect.width, 0) : Vector2.zero, .1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _turretManager.ShowPanel(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _turretManager.ShowPanel(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OpenPanel(false);
    }
}
