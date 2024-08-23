using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingInfoUI : MonoBehaviour
{
    private string lastMenu;
    [SerializeField] private MenuController menuController;
    [SerializeField] private Transform infoParent;

    private void Start()
    {
        menuController.OnMenuChange += MenuController_OnMenuChange;
        PlayerGridModify.BeingSlotSelected += PlayerGridModify_BeingSlotSelected;
        PlayerGridModify.BeingSlotCleared += PlayerGridModify_BeingSlotCleared;

        PlayerGridModify_BeingSlotCleared(null);
    }

    private void OnDisable()
    {
        menuController.OnMenuChange -= MenuController_OnMenuChange;
        PlayerGridModify.BeingSlotSelected -= PlayerGridModify_BeingSlotSelected;
        PlayerGridModify.BeingSlotCleared -= PlayerGridModify_BeingSlotCleared;
    }

    private void PlayerGridModify_BeingSlotCleared(BeingSlot obj)
    {
        infoParent.gameObject.SetActive(false);
    }

    private void PlayerGridModify_BeingSlotSelected(BeingSlot obj)
    {
        infoParent.gameObject.SetActive(true);
    }


    private void MenuController_OnMenuChange(Menu obj)
    {
        lastMenu = obj.MenuName;
    }
}