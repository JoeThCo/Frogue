using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private static Effect[] allEffects;

    public static bool isLoaded { get; private set; } = false;

    public static void LoadResources()
    {
        if (isLoaded) return;

        allEffects = Resources.LoadAll<Effect>("Effect");

        isLoaded = true;
    }

    public static Effect GetEffect()
    {
        return GetRandom<Effect>(allEffects);
    }


    private static T FindByName<T>(T[] scriptableObjects, string name) where T : ScriptableObject
    {
        foreach (T obj in scriptableObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    private static T GetRandom<T>(T[] scriptableObjects) where T : ScriptableObject
    {
        return scriptableObjects[GameManager.Random.Next(0, scriptableObjects.Length)];
    }
}