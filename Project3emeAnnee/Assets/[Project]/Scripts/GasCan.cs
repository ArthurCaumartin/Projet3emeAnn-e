using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCan : MonoBehaviour
{
    [SerializeField] private int _gasToGet = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PartyManager.instance.AddGas(_gasToGet, false);
            Destroy(gameObject);
        }
    }
}
