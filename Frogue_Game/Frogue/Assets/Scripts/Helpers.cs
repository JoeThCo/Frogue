using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static T[] Flatten<T>(T[,] input)
    {
        int rows = input.GetLength(0);
        int cols = input.GetLength(1);
        T[] flatArray = new T[rows * cols];

        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                flatArray[index++] = input[i, j];
            }
        }
        return flatArray;
    }
}
