using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotDisplay : MonoBehaviour
{
    public Vector2Int Coords { get; private set; }
    public bool IsPlayerInteractable { get; private set; }

    public void SlotDisplayInit(Vector2Int coords, bool isPlayerInteractable)
    {
        Coords = coords;
        IsPlayerInteractable = isPlayerInteractable;
    }
}