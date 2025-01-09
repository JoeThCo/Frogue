using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeingDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textID;

    public Vector2Int Coords { get; set; }

    public void BeingDisplayInit(Being being, bool isPlayerInteractable)
    {
        Coords = being.Coords;
        gameObject.name = being.ID.ToString();

        textID.SetText(being.ID.ToString());
        textID.transform.LookAt(-GameManager.MainCamera.transform.position);

        GameManager.SetInteractableTag(gameObject, isPlayerInteractable);
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        BeingDisplay otherDisplay = other as BeingDisplay;
        return Coords.Equals(otherDisplay.Coords);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}