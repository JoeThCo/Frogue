using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGridModify : MonoBehaviour
{
    [SerializeField] private BeingHolder playerGrid;
    [SerializeField] private BeingSlot selectedSlot;

    bool canInteract = true;

    public static event Action<BeingSlot> BeingSlotSelected;
    public static event Action<BeingSlot> BeingSlotSwapped;
    public static event Action<BeingSlot> BeingSlotCleared;

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        BeingBattle.FightStart += BeingBattleBus_BattleStart;
        BeingBattle.FightEnd += BeingBattleBus_BattleEnd;

        BeingSlotSelected += BeingGridModify_BeingSlotSelected;
        BeingSlotSwapped += BeingGridModify_BeingSlotSwapped;
        BeingSlotCleared += BeingGridModify_BeingSlotCleared;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        BeingBattle.FightStart -= BeingBattleBus_BattleStart;
        BeingBattle.FightEnd -= BeingBattleBus_BattleEnd;

        BeingSlotSelected -= BeingGridModify_BeingSlotSelected;
        BeingSlotSwapped -= BeingGridModify_BeingSlotSwapped;
        BeingSlotCleared -= BeingGridModify_BeingSlotCleared;

        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void BeingGridModify_BeingSlotSwapped(BeingSlot otherBeingSlot)
    {
        selectedSlot.SwapBeings(otherBeingSlot);
        BeingSlotCleared?.Invoke(otherBeingSlot);
        SoundEffectsManager.PlaySFX("SlotSwapped", otherBeingSlot);
    }

    private void BeingGridModify_BeingSlotCleared(BeingSlot otherBeingSlot)
    {
        selectedSlot.OnDeselect();

        if (otherBeingSlot.Equals(selectedSlot))
            SoundEffectsManager.PlaySFX("SlotCleared", otherBeingSlot);

        selectedSlot = null;
    }

    private void BeingGridModify_BeingSlotSelected(BeingSlot otherBeingSlot)
    {
        selectedSlot = otherBeingSlot;
        selectedSlot.OnSelect();
        playerGrid.DisplaySelectionSlots(otherBeingSlot);
        SoundEffectsManager.PlaySFX("SlotSelected", otherBeingSlot);
    }

    private void BeingBattleBus_BattleStart()
    {
        canInteract = false;
        selectedSlot = null;
    }

    private void BeingBattleBus_BattleEnd()
    {
        canInteract = true;
    }

    private void Update()
    {
        if (!canInteract) return;

        if (Input.GetKeyDown(KeyCode.Space) && selectedSlot != null)
            Debug.Log(selectedSlot.Coords);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                BeingSlot otherBeingSlot = hit.collider.gameObject.GetComponent<BeingSlot>();
                if (otherBeingSlot == null) return;
                if (!otherBeingSlot.PlayerInteractable) return;

                if (selectedSlot == null)
                {
                    if (otherBeingSlot.Being == null) return;
                    BeingSlotSelected?.Invoke(otherBeingSlot);
                }
                else
                {
                    if (selectedSlot.Equals(otherBeingSlot))
                        BeingSlotCleared?.Invoke(otherBeingSlot);
                    else
                        BeingSlotSwapped?.Invoke(otherBeingSlot);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedSlot != null)
            BeingSlotCleared?.Invoke(selectedSlot);
    }

}