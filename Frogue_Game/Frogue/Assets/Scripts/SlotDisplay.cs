using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotDisplay : MonoBehaviour
{
    public Vector2Int Coords { get; private set; }

    public void SlotDisplayInit(Vector2Int coords)
    {
        this.Coords = coords;
    }
}