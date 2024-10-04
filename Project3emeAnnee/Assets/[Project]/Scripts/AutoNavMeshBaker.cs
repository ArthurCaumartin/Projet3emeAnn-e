using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class AutoNavMeshBaker : MonoBehaviour
{

    private void Start()
    {
        NavMeshSurface n = GetComponent<NavMeshSurface>();
        if(!n.navMeshData) n.BuildNavMesh();
    }
}
