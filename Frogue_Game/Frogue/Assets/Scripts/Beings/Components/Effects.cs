using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects
{
    private List<Effect> AllEffects = new List<Effect>();

    public Effects(Effect[] effects)
    {
        AllEffects = new List<Effect>(effects);
    }

    public void AddEffect(Effect[] effects)
    {
        foreach (Effect effect in effects)
        {
            AllEffects.Add(effect);
        }
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