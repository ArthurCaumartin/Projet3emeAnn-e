using UnityEngine;
using UnityEngine.Serialization;

public class TurretBaker : MonoBehaviour
{
    [Header("Turret Scriptable :")]
    [SerializeField] private ScriptableCannon _cannon;
    [SerializeField] private ScriptaleCore _core;
    [SerializeField] private ScriptableBody _body; 

    [Header("Mesh Renderer :")]
    [SerializeField] private VisualBaker _cannonRenderer;
    [SerializeField] private VisualBaker _coreRenderer;
    [SerializeField] private VisualBaker _bodyRenderer;

    [Header("Other :")]
    [SerializeField] private TargetFinder _finder;

    private void Start()
    {
        BakeAll();
    }

    public void SetTurretComponent(ScriptableTurretPart partToSet)
    {
        switch (partToSet)
        {
            case ScriptableCannon:
                _cannon = partToSet as ScriptableCannon;
                break;
            case ScriptaleCore:
                _core = partToSet as ScriptaleCore;
                break;
            case ScriptableBody:
                _body = partToSet as ScriptableBody;
                break;
        }
        BakeAll();
    }

    private void BakeAll()
    {
        _cannonRenderer.Bake(_cannon);
        _coreRenderer.Bake(_core);
        _bodyRenderer.Bake(_body);

        _finder.Bake(_cannon.turretCannon, _body.stat);
    }
}
