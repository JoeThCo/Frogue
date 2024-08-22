using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects
{
    private List<Effect> AllEffects = new List<Effect>();

    private Being being;

    public Effects(Being being, Effect[] effects)
    {
        this.being = being;
        AllEffects = new List<Effect>(effects);
    }

    public void AddEffect(Effect effect)
    {
        effect.OnEffectAdded(being);
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