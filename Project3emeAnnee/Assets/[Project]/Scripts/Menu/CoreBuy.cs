using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBuy : MonoBehaviour
{
    public GasolineManager _gasManager;
    public int _coreValue;
    public GameObject _coreSprite, _coreBuy;

    private void Start()
    {
        _gasManager = GetComponentInParent<GasolineManager>();
    }

    public void BuyCore()
    {
        if (_gasManager.BuyCore(_coreValue))
        {
            _coreBuy.SetActive(false);
            _coreSprite.SetActive(true);
        }
    }
}
