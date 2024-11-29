using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ResourceLoader
{
    private static BeingInfo[] allBeingInfo;

    public static bool IsLoaded { get; private set; } = false;

    public static void Load()
    {
        if (IsLoaded) return;

        allBeingInfo = Resources.LoadAll<BeingInfo>("BeingInfo");
        Debug.Log($"Being Info: {allBeingInfo.Length}");

        IsLoaded = true;
    }

    private static T GetRandom<T>(T[] array) where T : ScriptableObject
    {
        if (array == null || array.Length == 0)
        {
            Debug.LogWarning("The provided array is null or empty.");
            return null;
        }

        int randomIndex = BattleManager.Random.Next(0, array.Length);
        return array[randomIndex];
    }

    public static BeingInfo GetBeingInfo() { return GetRandom<BeingInfo>(allBeingInfo); }
}