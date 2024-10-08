using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject _mobToInstantiate;
    [SerializeField] private int _numberEnemiesToSpawn;

    private void Start()
    {
        GetComponent<MobHealth>().OnDeathEvent.AddListener(GetDestroyed);
    }

    public void GetDestroyed(MobHealth noUSe)
    {
        for (int i = 0; i < _numberEnemiesToSpawn; i++)
        {
            float tempFloatX = Random.Range(-3, 3);
            float tempFloatZ = Random.Range(-3, 3);
            
            GameObject mobInstantiate = Instantiate(_mobToInstantiate, new Vector3(
                transform.position.x + tempFloatX, transform.position.y, transform.position.z + tempFloatZ
                ), quaternion.identity);
            MobHealth mobHealth = mobInstantiate.GetComponent<MobHealth>();
            mobHealth.GetComponent<Mob>().Initialize(PlayerInstance.instance.transform);
        }
    }
}
