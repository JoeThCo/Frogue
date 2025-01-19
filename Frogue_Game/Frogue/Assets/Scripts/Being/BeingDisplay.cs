using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeingDisplay : MonoBehaviour
{
    [SerializeField] private Transform Model;
    [Space(10)]
    [SerializeField] BeingDisplayUI UI;
    public Vector2Int Coords { get; set; }

    public void BeingDisplayInit(Being being, bool isPlayerInteractable)
    {
        Coords = being.Coords;
        gameObject.name = being.ID.ToString();
        UI.BeingDisplayUIInit(being);

        GameManager.SetInteractableTag(gameObject, isPlayerInteractable);
    }

    public void Selected() 
    {
        Model.DOScale(1.35f, .25f).SetEase(Ease.Linear);
    }

    public void DeSelect() 
    {
        Model.DOScale(1, .25f).SetEase(Ease.Linear);
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