using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Color RandomColor()
    {
        float r = (float)GameManager.Random.NextDouble();
        float b = (float)GameManager.Random.NextDouble();
        float g = (float)GameManager.Random.NextDouble();
        return new Color(r, g, b, 1);
    }

    public static T FindByName<T>(T[] gameObjects, string name) where T : UnityEngine.Object
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

    public static T GetRandom<T>(T[] gameObjects) where T : UnityEngine.Object
    {
        return gameObjects[GameManager.Random.Next(0, gameObjects.Length)];
    }
}