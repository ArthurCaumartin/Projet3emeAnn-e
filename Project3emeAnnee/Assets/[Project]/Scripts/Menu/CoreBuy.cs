using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBuy : MonoBehaviour
{
    private ResearchManager _researchManager;
    public int _coreValue;
    public GameObject _corePrefab;

    private void Start()
    {
        _researchManager = GetComponentInParent<ResearchManager>();
    }

    // Call Research Manager to verify if we have enough Gas to buy this core
    // If Yes, instantiate a core sprite that we can Drag&Drop onto mecha parts to upgrades them
    public void BuyCore()
    {
        if (_researchManager.BuyCore(_coreValue))
        {
            Instantiate(_corePrefab, transform);
        }
    }
}
