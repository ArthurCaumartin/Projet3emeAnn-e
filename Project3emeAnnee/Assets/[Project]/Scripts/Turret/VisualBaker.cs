using UnityEngine;

public class VisualBaker : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private MeshFilter _mesh;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _mesh = GetComponent<MeshFilter>();
    }

    public void Bake(ScriptableTurretPart part)
    {
        _mesh.mesh = part.mesh;
    }
}