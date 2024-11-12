using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeingDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;

    public Being Being { get; private set; }

    public void BeingDisplayInit(Being being)
    {
        this.Being = being;
        debugText.SetText(being.ID.ToString());
    }
}