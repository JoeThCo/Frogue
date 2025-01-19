using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotDisplay : MonoBehaviour
{
    public Transform BeingOffset;
    [SerializeField] private Transform Model;
    public Vector2Int Coords { get; set; }
    public BeingDisplay BeingDisplay { get; set; }

    public void SlotDisplayInit(Vector2Int coords, bool isPlayerInteractable)
    {
        this.Coords = coords;
        gameObject.name = coords.ToString();
        GameManager.SetInteractableTag(gameObject, isPlayerInteractable);

        Model.rotation = Quaternion.Euler(Vector3.up * Random.value * 360);
    }
}