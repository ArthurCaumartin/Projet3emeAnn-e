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

    void Start()
    {
        _mainCam = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _mouseMoveAction = GetComponent<PlayerInput>().actions.FindAction("MouseMove");
    }

    void Update()
    {
        if(RaycasterUI.instance.GetTypeUnderMouse<Image>()) print("Over UI");
        if(_mouseMoveAction.ReadValue<float>() > .5f)
            _agent.destination = GetMouseGroundPos();
    }

    public Vector3 GetMouseGroundPos()
    {
        if(RaycasterUI.instance.GetTypeUnderMouse<Image>()) return _agent.destination;
        Physics.Raycast(_mainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        if(!hit.collider) return _agent.destination;
        print(hit.collider.name);
        return hit.point;
    }
}
