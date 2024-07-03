using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public List<Effect> AllEffects { get; private set; } = new List<Effect>();

    public void EffectInit()
    {
        AllEffects.Add(ResourceManager.GetEffect());
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

        Debug.Log("From " + input + " -> " + output);
        return output;
    }
}