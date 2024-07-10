using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingSlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer outline;
    public BeingController BeingController { get; set; }
    public Vector2Int Coords { get; private set; }
    public bool isPlayerInteractable { get; private set; } = false;

    private Vector3 defaultScale = Vector3.zero;

    public void BeingSlotInit(Vector2Int coords, bool isPlayerInteractable)
    {
        this.Coords = coords;
        this.isPlayerInteractable = isPlayerInteractable;
        this.defaultScale = outline.transform.localScale;

        OnDeselect();
    }

    public override string ToString()
    {
        return BeingController.ToString() + " " + Coords.ToString();
    }

    public void OnSelect()
    {
        outline.color = Color.white;
        outline.transform.localScale = defaultScale * 1.25f;
    }

    public void OnDeselect()
    {
        outline.color = Color.black;
        outline.transform.localScale = defaultScale;
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        BeingSlot compare = other as BeingSlot;

        return compare.Coords == Coords &&
            compare.isPlayerInteractable == isPlayerInteractable &&
            compare.BeingController == BeingController;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}