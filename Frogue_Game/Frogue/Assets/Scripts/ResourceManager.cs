using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    public static bool isLoaded { get; private set; } = false;

    public static void LoadResources()
    {
        if (isLoaded) return;

        isLoaded = true;
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
        return scriptableObjects[UnityEngine.Random.Range(0, scriptableObjects.Length)];
    }
}