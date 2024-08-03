using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeingInfoUI : MonoBehaviour
{
    [SerializeField] private Transform infoPanel;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI typeText;

    private void Start()
    {
        BeingGridModify.BeingSlotSelected += BeingGridModify_BeingSlotSelected;
        BeingGridModify.BeingSlotCleared += BeingGridModify_BeingSlotCleared;

        BeingGridModify_BeingSlotCleared();
    }

    private void OnDisable()
    {
        BeingGridModify.BeingSlotSelected -= BeingGridModify_BeingSlotSelected;
        BeingGridModify.BeingSlotCleared -= BeingGridModify_BeingSlotCleared;
    }

    private void BeingGridModify_BeingSlotSelected(BeingSlot obj)
    {
        if (obj == null) return;
        if (obj.Being == null) return;

        nameText.SetText(obj.Being.BeingInfo.name);
        damageText.SetText($"D: {obj.Being.Damage}");
        healthText.SetText(obj.Being.Health.ToString());
        typeText.SetText(obj.Being.Types.ToString());

        infoPanel.gameObject.SetActive(true);
    }

    private void BeingGridModify_BeingSlotCleared()
    {
        infoPanel.gameObject.SetActive(false);
    }
}
