using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : IFinalValue
{
    private int SpeedAmount;

    private Effects effects;

    public Speed(Effects effects, int startSpeed)
    {
        this.effects = effects;
        this.SpeedAmount = startSpeed;
    }

    public int GetFinalValue()
    {
        return effects.GetFinalValue(SpeedAmount, typeof(SpeedChange));
    }
}