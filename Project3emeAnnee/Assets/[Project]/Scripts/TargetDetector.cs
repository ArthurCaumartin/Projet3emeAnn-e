using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TargetDetector : MonoBehaviour 
{
    //TODO refacto le prefab
    [SerializeField] private Transform _target;
    [SerializeField] private float _triggerDistance;
    [SerializeField] private UnityEvent<bool> _event;
    public UnityEvent<bool> TriggerEnvent { get => _event; }
    [SerializeField] private Color _gizmoColor = new Color(0, 1, 1, .5f);
    private bool _isTrigger = false;

    void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, _target.position);
        if (currentDistance <= _triggerDistance)
        {
            if (_isTrigger == true) return;
            _isTrigger = true;
            _event.Invoke(true);
        }
        else
        {
            if (_isTrigger == false) return;
            _isTrigger = false;
            _event.Invoke(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawSphere(transform.position, _triggerDistance);
    }
}
