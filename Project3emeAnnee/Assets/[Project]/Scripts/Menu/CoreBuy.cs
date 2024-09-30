using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBuy : MonoBehaviour
{
    private ResearchManager _gasManager;
    public int _coreValue;
    public GameObject _corePrefab;

    private void Start()
    {
        _gasManager = GetComponentInParent<ResearchManager>();
    }

    public void BuyCore()
    {
        if (_gasManager.BuyCore(_coreValue))
        {
            Instantiate(_corePrefab, transform);
        }
    }
}
