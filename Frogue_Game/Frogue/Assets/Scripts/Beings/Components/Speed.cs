using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public int SpeedAmount { get; private set; }

    private Effects effects;

    public void SpeedInit(Effects effects)
    {
        this.effects = effects;
    }

    public int GetSpeed()
    {
        return effects.GetFinalValue(SpeedAmount, typeof(SpeedChange));
    }
}