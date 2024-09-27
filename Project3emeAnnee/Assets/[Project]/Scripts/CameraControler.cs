using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [Space]
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _posOffset;
    [SerializeField] private float _followSpeed = 5f;
    private Vector3 _velocity;

    private void Start()
    {
        if (!_camera) _camera = Camera.main;
    }

    private void OnValidate()
    {
        if (_target)
            transform.position = _target.position + _posOffset;
    }

    public void LookAtTarget()
    {
        if (_target)
            transform.LookAt(_target);
    }

    private void Update()
    {
        Vector3 targetPos = _target.position + _posOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, 1 / _followSpeed, Mathf.Infinity);
    }
}
