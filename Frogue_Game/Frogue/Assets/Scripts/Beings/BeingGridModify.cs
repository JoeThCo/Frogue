using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class BeingGridModify : MonoBehaviour
{
    [SerializeField] private PlayerGrid beingGrid;
    [SerializeField] private BeingSlot selectedSlot;

    bool canInteract = true;

    public static event Action<BeingSlot> BeingSlotSelected;
    public static event Action<BeingSlot> BeingSlotSwapped;
    public static event Action BeingSlotCleared;

    private void Start()
    {
        BeingBattle.FightStart += BeingBattleBus_BattleStart;
        BeingBattle.FightEnd += BeingBattleBus_BattleEnd;

        BeingSlotSelected += BeingGridModify_BeingSlotSelected;
        BeingSlotSwapped += BeingGridModify_BeingSlotSwapped;
        BeingSlotCleared += BeingGridModify_BeingSlotCleared;
    }

    private void OnDisable()
    {
        BeingBattle.FightStart -= BeingBattleBus_BattleStart;
        BeingBattle.FightEnd -= BeingBattleBus_BattleEnd;

        BeingSlotSelected -= BeingGridModify_BeingSlotSelected;
        BeingSlotSwapped -= BeingGridModify_BeingSlotSwapped;
        BeingSlotCleared -= BeingGridModify_BeingSlotCleared;
    }

    private void BeingGridModify_BeingSlotSwapped(BeingSlot otherBeingSlot)
    {
        selectedSlot.SwapBeings(otherBeingSlot);
        BeingSlotCleared?.Invoke();
    }

    private void BeingGridModify_BeingSlotCleared()
    {
        if (selectedSlot == null) return;
        selectedSlot.OnDeselect();
        selectedSlot = null;
    }

    private void BeingGridModify_BeingSlotSelected(BeingSlot otherBeingSlot)
    {
        if (!otherBeingSlot.Being) return;
        selectedSlot = otherBeingSlot;
        selectedSlot.OnSelect();
    }

    private void BeingBattleBus_BattleStart()
    {
        canInteract = false;
    }

    private void BeingBattleBus_BattleEnd()
    {
        canInteract = true;
    }

    private void Update()
    {
        if (!canInteract) return;

        if (Input.GetKeyDown(KeyCode.Space) && selectedSlot != null)
        {
            Debug.Log(selectedSlot.Coords);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null) return;

            BeingSlot otherBeingSlot = hit.collider.gameObject.GetComponent<BeingSlot>();
            if (otherBeingSlot == null) return;
            if (!otherBeingSlot.isPlayerInteractable) return;

            if (!selectedSlot)
            {
                BeingSlotSelected?.Invoke(otherBeingSlot);
            }
            else
            {
                BeingSlotSwapped?.Invoke(otherBeingSlot);
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            BeingSlotCleared?.Invoke();
        }
    }
}