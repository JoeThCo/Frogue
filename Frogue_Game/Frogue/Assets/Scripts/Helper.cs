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

        Debug.LogFormat("{0}, {1}, {2}", r, g, b);
        return new Color(r, g, b, 1);
    }
}