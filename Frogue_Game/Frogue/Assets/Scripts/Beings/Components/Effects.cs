using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects
{
    public List<Effect> AllEffects { get; private set; } = new List<Effect>();

    public void EffectInit()
    {
        AllEffects.Add(ResourceManager.GetEffect());
    }

    public void AddEffect(Effect effect)
    {
        AllEffects.Add(effect);
    }

    public void RemoveEffect(Effect effect)
    {
        if (AllEffects.Contains(effect))
            AllEffects.Remove(effect);
    }

    public int GetFinalValue(int input, Type type)
    {
        int output = input;

        foreach (Effect effect in AllEffects)
        {
            if (effect.GetType().IsAssignableFrom(type))
            {
                output += effect.GetChange();
            }
        }
        return output;
    }
}