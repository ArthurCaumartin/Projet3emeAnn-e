using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

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
        if(_mouseMoveAction.ReadValue<float>() > .5f)
            _agent.destination = GetMouseGroundPos();

    }

    public Vector3 GetMouseGroundPos()
    {
        Physics.Raycast(_mainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        if(!hit.collider) return transform.position;
        print(hit.collider.name);
        return hit.point;
    }
}
