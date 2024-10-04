using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobClass : ScriptableObject
{
    public string mobName;
    public int mobHealth;
    public float mobMovementSpeed, mobScale;

    public virtual void DeathEffect()
    {
        
    }
}
