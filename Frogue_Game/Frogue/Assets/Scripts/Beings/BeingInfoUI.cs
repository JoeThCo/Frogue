using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BeingInfoUI : MonoBehaviour
{
    [SerializeField] private Transform infoPanel;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI typeText;
    [Space(10)]
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        PlayerGridModify.BeingSlotSelected += BeingGridModify_BeingSlotSelected;
        PlayerGridModify.BeingSlotCleared += BeingGridModify_BeingSlotCleared;

        BeingGridModify_BeingSlotCleared(null);
    }

    private void OnDisable()
    {
        PlayerGridModify.BeingSlotSelected -= BeingGridModify_BeingSlotSelected;
        PlayerGridModify.BeingSlotCleared -= BeingGridModify_BeingSlotCleared;
    }

    private void BeingGridModify_BeingSlotSelected(BeingSlot beingSlot)
    {
        if (beingSlot == null) return;
        if (beingSlot.Being == null) return;

        nameText.SetText(beingSlot.Being.BeingInfo.name);
        damageText.SetText($"Damage: {beingSlot.Being.Damage}");
        typeText.SetText(beingSlot.Being.Types.ToString());

        healthBar.HealthBarInit(beingSlot.Being);

        infoPanel.gameObject.SetActive(true);
    }

    private void BeingGridModify_BeingSlotCleared(BeingSlot beingSlot)
    {
        infoPanel.gameObject.SetActive(false);
    }
}