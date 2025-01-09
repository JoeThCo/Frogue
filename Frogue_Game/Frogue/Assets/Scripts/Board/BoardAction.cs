using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardAction
{
    public Vector2Int From { get; protected set; }
    public Vector2Int To { get; protected set; }

    public abstract void Display();
}
