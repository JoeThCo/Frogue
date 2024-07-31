using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private static FrogSO[] allFrogs;
    private static BaddieSO[] allBaddies;

    private static BulkAttack[] allBulkAttacks;
    private static Effect[] allEffects;
    private static GameObject[] allUI;

    public static bool isLoaded { get; private set; } = false;

    public static void LoadResources()
    {
        if (isLoaded) return;

        allEffects = Resources.LoadAll<Effect>("Effect");
        allUI = Resources.LoadAll<GameObject>("UI");
        allBulkAttacks = Resources.LoadAll<BulkAttack>("BulkAttack");

        allBaddies = Resources.LoadAll<BaddieSO>("Baddie");
        allFrogs = Resources.LoadAll<FrogSO>("Frog");

        isLoaded = true;
    }

    public static BulkAttack GetBulkAttack()
    {
        return GetRandom<BulkAttack>(allBulkAttacks);
    }

    public static FrogSO GetFrog()
    {
        return GetRandom<FrogSO>(allFrogs);
    }

    public static BaddieSO GetBaddie()
    {
        return GetRandom<BaddieSO>(allBaddies);
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