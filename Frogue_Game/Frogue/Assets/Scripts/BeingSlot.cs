using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingSlot : MonoBehaviour
{
    public Being Being { get; set; }
    public Vector2Int Coords { get; private set; }

    public void BeingSlotInit(Vector2Int coords)
    {
        this.Coords = coords;
    }

    public override string ToString()
    {
        return Being.ToString() + " " + Coords.ToString();
    }
}