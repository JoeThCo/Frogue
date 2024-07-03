using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int DamageAmount { get; private set; }

    private Effects effects;

    public void DamageInit(Effects effects)
    {
        this.effects = effects;
        DamageAmount = Random.Range(2, 7);
    }

    public int GetDamage()
    {
        return effects.GetFinalValue(DamageAmount, typeof(DamageChange));
    }
}