using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private static Trigger[] allTriggers;
    private static Who[] allWho;
    private static Effect[] allEffects;

    public static bool isLoaded { get; private set; } = false;

    public static void LoadResources()
    {
        if (isLoaded) return;

        allTriggers = Resources.LoadAll<Trigger>("Trigger");
        allWho = Resources.LoadAll<Who>("Who");
        allEffects = Resources.LoadAll<Effect>("Effect");

        isLoaded = true;
    }

    public static Effect GetEffect(string searchName)
    {
        return FindByName<Effect>(allEffects, searchName);
    }

    public static Effect GetEffect()
    {
        return GetRandom<Effect>(allEffects);
    }

    public static Trigger GetTrigger(string searchName)
    {
        return FindByName<Trigger>(allTriggers, searchName);
    }

    public static Trigger GetTrigger()
    {
        return GetRandom<Trigger>(allTriggers);
    }

    public static Who GetWho(string searchName)
    {
        return FindByName<Who>(allWho, searchName);
    }

    public static Who GetWho()
    {
        return GetRandom<Who>(allWho);
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