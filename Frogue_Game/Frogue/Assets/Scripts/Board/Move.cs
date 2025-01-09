using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : BoardAction
{
    public Move(Vector2Int from, Vector2Int to) : base()
    {
        From = from;
        To = to;
    }

    public override void Display()
    {
        Debug.Log($"Move: {From} -> {To}");
    }
}
