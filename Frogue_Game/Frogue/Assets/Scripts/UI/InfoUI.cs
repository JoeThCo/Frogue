using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI typeText;
    [Space(10)]
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        PlayerGridModify.BeingSlotSelected += BeingGridModify_BeingSlotSelected;
    }

    private void OnDisable()
    {
        PlayerGridModify.BeingSlotSelected -= BeingGridModify_BeingSlotSelected;
    }

    private void BeingGridModify_BeingSlotSelected(BeingSlot beingSlot)
    {
        nameText.SetText(beingSlot.Being.BeingInfo.name);
        damageText.SetText($"Damage: {beingSlot.Being.Damage}");
        typeText.SetText(beingSlot.Being.Types.ToString());

        healthBar.HealthBarInit(beingSlot.Being);
    }
}