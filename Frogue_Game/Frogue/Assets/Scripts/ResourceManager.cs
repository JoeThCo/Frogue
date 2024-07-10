using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private static Being[] allBeings;

    private static Effect[] allEffects;
    private static GameObject[] allUI;

    public static bool isLoaded { get; private set; } = false;

    public static void LoadResources()
    {
        if (isLoaded) return;

        allEffects = Resources.LoadAll<Effect>("Effect");
        allUI = Resources.LoadAll<GameObject>("UI");
        allBeings = Resources.LoadAll<Being>("Being");

        isLoaded = true;
    }

    public static Being GetBeing()
    {
        Being random = GetRandom<Being>(allBeings);
        random.BeingInit();
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
        throw new System.Exception($"No Object with name of {name}");
    }

    private static T GetRandom<T>(T[] gameObjects) where T : UnityEngine.Object
    {
        return gameObjects[GameManager.Random.Next(0, gameObjects.Length)];
    }
}