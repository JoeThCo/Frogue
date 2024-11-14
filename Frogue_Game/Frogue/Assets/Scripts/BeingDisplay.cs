using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeingDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private Transform rotateTransform;

    public Being Being { get; private set; }
    public bool IsPlayerInteractable { get; private set; }

    public void BeingDisplayInit(Being being, bool isPlayerInteractable)
    {
        Being = being;
        IsPlayerInteractable = isPlayerInteractable;

        debugText.SetText(being.ID.ToString());
    }

    public void Rotate(float y)
    {
        rotateTransform.Rotate(Vector3.up * y);
    }
}