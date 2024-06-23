using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingSlot : MonoBehaviour
{
    public Being Being { get; set; }
    public Vector2Int coords { get; private set; }

    public void BeingSlotInit(Vector2Int coords)
    {
        this.coords = coords;
    }
}