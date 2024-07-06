using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : IFinalValue
{
    private int DamageAmount;

    private Effects effects;

    public Damage(Effects effects) 
    {
        this.effects = effects;
        DamageAmount = Random.Range(2, 7);
    }

    public int GetFinalValue()
    {
        return effects.GetFinalValue(DamageAmount, typeof(DamageChange));
    }
}