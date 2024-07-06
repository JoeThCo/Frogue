using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private static Effect[] allEffects;
    private static BeingType[] allBeingTypes;

    private static GameObject[] allUI;

    public static bool isLoaded { get; private set; } = false;

    public static void LoadResources()
    {
        if (isLoaded) return;

        allEffects = Resources.LoadAll<Effect>("Effect");
        allUI = Resources.LoadAll<GameObject>("UI");
        allBeingTypes = Resources.LoadAll<BeingType>("Types");

        isLoaded = true;
    }

    public static BeingType GetBeingType()
    {
        BeingType random = GetRandom<BeingType>(allBeingTypes);
        random.TypeInit();
        return random;
    }

    public static Effect GetEffect()
    {
        return GetRandom<Effect>(allEffects);
    }

    public static GameObject GetUI(string name)
    {
        return FindByName<GameObject>(allUI, name);
    }

    private static T FindByName<T>(T[] gameObjects, string name) where T : UnityEngine.Object
    {
        foreach (T obj in gameObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    private static T GetRandom<T>(T[] gameObjects) where T : UnityEngine.Object
    {
        return gameObjects[GameManager.Random.Next(0, gameObjects.Length)];
    }
}