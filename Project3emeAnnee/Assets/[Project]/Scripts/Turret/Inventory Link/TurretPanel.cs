using UnityEngine;
using UnityEngine.EventSystems;

public class TurretPanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TurretBaker _turretBaker;
    [SerializeField] private RectTransform _panelContainer;
    private TurretManager _turretManager;
    public TurretBaker TurretBaker { get => _turretBaker; set => _turretBaker = value; }

    private void Start()
    {
        _panelContainer.gameObject.SetActive(false);
    }

    public void Initialize(TurretManager manager, TurretBaker baker)
    {
        _turretManager = manager;
        _turretBaker = baker;
    }

    public void ChangeTurretPart(ScriptableTurretPart part)
    {
        _turretBaker.SetTurretComponent(part);
    }

    public void OpenPanel(bool value)
    {
        _panelContainer.gameObject.SetActive(value);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _turretManager.ClicPanel(this);
    }
}
