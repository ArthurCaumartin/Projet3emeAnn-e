using UnityEngine;

public class MobileTurret : MonoBehaviour
{
    [SerializeField] private float _distanceFromPlayer = 5;
    [SerializeField] private float _rotateSpeed = 3;
    [SerializeField] private float _height = .5f;
    private Transform _playerTransform;
    private Vector3 _posOffset;
    private float _rotateTimeOffset;

    public MobileTurret Initialize(Transform playerTransform, float timeOffset)
    {
        _playerTransform = playerTransform;
        _rotateTimeOffset = Mathf.Lerp(0, 360, timeOffset);
        return this;
    }

    private void Update()
    {
        float angleOnTime = (Time.time * _rotateSpeed) + _rotateTimeOffset;
        _posOffset = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angleOnTime), 0, Mathf.Cos(Mathf.Deg2Rad * angleOnTime));
        _posOffset = _posOffset.normalized;
        _posOffset.y = _height;
        transform.position = _playerTransform.position + (_posOffset * _distanceFromPlayer);
    }
}
