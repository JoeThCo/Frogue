using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingInfoUI : MonoBehaviour
{
    [SerializeField] private Transform infoParent;

    private void Start()
    {
        PlayerGridModify.BeingSlotSelected += PlayerGridModify_BeingSlotSelected;
        PlayerGridModify.BeingSlotCleared += PlayerGridModify_BeingSlotCleared;

        PlayerGridModify_BeingSlotCleared(null);
    }

    private void OnDisable()
    {
        PlayerGridModify.BeingSlotSelected -= PlayerGridModify_BeingSlotSelected;
        PlayerGridModify.BeingSlotCleared -= PlayerGridModify_BeingSlotCleared;
    }

    private void PlayerGridModify_BeingSlotSelected(BeingSlot obj)
    {
        infoParent.gameObject.SetActive(true);
    }

    private void PlayerGridModify_BeingSlotCleared(BeingSlot obj)
    {
        infoParent.gameObject.SetActive(false);
    }
}