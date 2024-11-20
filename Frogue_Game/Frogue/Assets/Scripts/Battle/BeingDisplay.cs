using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeingDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [Space(10)]
    [SerializeField] private Transform rotateTransform;

    public Being Being { get; private set; }
    public bool IsPlayerInteractable { get; private set; }

    public void BeingDisplayInit(Being being, bool isPlayerInteractable)
    {
        Being = being;
        IsPlayerInteractable = isPlayerInteractable;

        hpText.SetText(being.HP.ToString());
        damageText.SetText(being.Damage.ToString());
    }

    public void Rotate(float y)
    {
        rotateTransform.Rotate(Vector3.up * y);
    }
}