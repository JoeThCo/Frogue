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
        BeingBattle.FightStart += BeingBattle_FightStart;

        PlayerGridModify_BeingSlotCleared(null);
    }

    private void OnDisable()
    {
        PlayerGridModify.BeingSlotSelected -= PlayerGridModify_BeingSlotSelected;
        PlayerGridModify.BeingSlotCleared -= PlayerGridModify_BeingSlotCleared;
        BeingBattle.FightStart -= BeingBattle_FightStart;
    }

    private void BeingBattle_FightStart()
    {
        infoParent.gameObject.SetActive(false);
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