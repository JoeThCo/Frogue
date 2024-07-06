using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : IFinalValue
{
    private int SpeedAmount;

    private Effects effects;

    public Speed(Effects effects) 
    {
        this.effects = effects;

    }

    public int GetFinalValue()
    {
        return effects.GetFinalValue(SpeedAmount, typeof(SpeedChange));
    }
}