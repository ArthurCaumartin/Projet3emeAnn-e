using System;
using DG.Tweening;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [Serializable]
    public class CameraData
    {
        public Vector3 offSet;
        public Vector3 angle;
    }

    [SerializeField] private Transform _target;
    [SerializeField] private float _transitionDuration = .5f;
    [SerializeField] private Camera _camera;
    [Header("Moving Parametre :")]
    [SerializeField] private bool _dampMovement = true;
    [SerializeField] private float _followSpeed = 5f;
    [Header("Data Set up :")]
    [SerializeField] private Vector3 _posOffset;
    [SerializeField] private CameraData _movingData;
    [SerializeField] private CameraData _towerdefenceData;
    private Vector3 _velocity;

    private void Start()
    {
        if (!_camera) _camera = Camera.main;
    }

    private void OnValidate()
    {
        if (_target) FollowTarget(false);
    }

    private void Update()
    {
        FollowTarget(_dampMovement);
    }

    private void FollowTarget(bool dampMovement)
    {
        if (dampMovement)
        {
            Vector3 targetPos = _target.position + _posOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, 1 / _followSpeed, Mathf.Infinity);
        }
        else
        {
            transform.position = _target.position + _posOffset;
        }
    }

    public void SetCameraOnState(GameState state)
    {
        switch (state)
        {
            case GameState.Mobile:
                DOTween.To((time) =>
                {
                    transform.eulerAngles = Vector3.Lerp(_towerdefenceData.angle, _movingData.angle, time);
                    _posOffset = Vector3.Lerp(_towerdefenceData.offSet, _movingData.offSet, time);
                }, 0, 1, _transitionDuration)
                .SetEase(Ease.Linear);
                break;

            case GameState.TowerDefence:
                DOTween.To((time) =>
                {
                    transform.eulerAngles = Vector3.Lerp(_movingData.angle, _towerdefenceData.angle, time);
                    _posOffset = Vector3.Lerp(_movingData.offSet, _towerdefenceData.offSet, time);
                }, 0, 1, _transitionDuration)
                .SetEase(Ease.Linear);
                break;
        }
    }

    //! ///////////////////////////////////////////////////
    //! ///////////////////////////////////////////////////
    //! CALL BY EDITOR CLASS
    public void LookAtTarget()
    {
        if (_target)
            transform.LookAt(_target);
    }

    public void SaveMovingData()
    {
        _movingData.offSet = _posOffset;
        _movingData.angle = transform.eulerAngles;
    }

    public void SaveTowerDefenceData()
    {
        _towerdefenceData.offSet = _posOffset;
        _towerdefenceData.angle = transform.eulerAngles;
    }

    public void Reset()
    {
        _posOffset = Vector3.zero;
        transform.rotation = Quaternion.identity;
        OnValidate();
    }
    //! ///////////////////////////////////////////////////
    //! ///////////////////////////////////////////////////
}
