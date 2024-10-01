using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    private Vector3 _target;
    private Camera _mainCam;
    private NavMeshAgent _agent;
    private InputAction _mouseMoveAction;
    private bool _canMove = true;


    void Start()
    {
        _mainCam = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _mouseMoveAction = GetComponent<PlayerInput>().actions.FindAction("MouseMove");
    }

    void Update()
    {
        if (!_canMove) return;
        if (_mouseMoveAction.ReadValue<float>() > .5f)
        {
            _target = GetMouseGroundPos();
            _agent.destination = _target;
        }
    }

    public Vector3 GetMouseGroundPos()
    {
        //! If mouse is over UI don't update destination;
        if (RaycasterUI.instance.GetTypeUnderMouse<Image>()) return _agent.destination;

        RaycastHit[] hits = Physics.RaycastAll(_mainCam.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, _groundLayer);
        if (hits.Length > 0 && hits[0].collider)
            return hits[0].point;
        return _agent.destination;
    }

    public void SetControlerInMobileMode()
    {
        _canMove = true;
    }

    public void SetControlerInSiphonMode(Transform siphonTransform)
    {
        _agent.destination = siphonTransform.position;
        _canMove = false;
    }
}
