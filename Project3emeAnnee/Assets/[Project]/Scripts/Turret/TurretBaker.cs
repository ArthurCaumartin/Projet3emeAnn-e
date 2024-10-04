using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class TurretBaker : MonoBehaviour
{
    [Header("Turret Scriptable :")]
    [SerializeField] private ScriptableCannon _canon;
    [SerializeField] private ScriptaleHeart _heart;
    [SerializeField] private ScriptableBase _base;

    [Header("Mesh Renderer :")]
    [SerializeField] private VisualBaker _cannonRenderer;
    [SerializeField] private VisualBaker _heartRenderer;
    [SerializeField] private VisualBaker _baseRenderer;

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
                _canon = partToSet as ScriptableCannon;
                break;
            case ScriptaleHeart:
                _heart = partToSet as ScriptaleHeart;
                break;
            case ScriptableBase:
                _base = partToSet as ScriptableBase;
                break;
        }
        BakeAll();
    }

    private void BakeAll()
    {
        _cannonRenderer.Bake(_canon);
        _heartRenderer.Bake(_heart);
        _baseRenderer.Bake(_base);

        _finder.Bake(_canon.turretCanon, _base.stat);
    }
}
