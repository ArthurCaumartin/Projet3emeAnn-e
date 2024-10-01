using UnityEngine;

public class VisualBaker : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private MeshFilter _meshFilter;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
    }

    public void Bake(ScriptableTurretPart part)
    {
        if (!_meshFilter || !_renderer) Start();
        _meshFilter.mesh = part.mesh;
    }
}