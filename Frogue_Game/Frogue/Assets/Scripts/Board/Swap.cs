using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : BoardAction
{
    public Swap(Vector2Int from, Vector2Int to) : base()
    {
        From = from;
        To = to;
    }

    public override string ToString()
    {
        return $"Swap from {From} to {To}";
    }

    public override void Display()
    {
        throw new System.NotImplementedException();
    }
}