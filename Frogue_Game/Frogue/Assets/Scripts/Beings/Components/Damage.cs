using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : IFinalValue
{
    private int DamageAmount;
    private Effects effects;

    public Damage(Effects effects, int startDamage) 
    {
        this.effects = effects;
        DamageAmount = startDamage;
    }

    public int GetFinalValue()
    {
        return effects.GetFinalValue(DamageAmount, typeof(DamageChange));
    }

    public override string ToString()
    {
        return GetFinalValue().ToString();
    }
}