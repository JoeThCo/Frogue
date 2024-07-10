using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class BeingGridModify : MonoBehaviour
{
    [SerializeField] private BeingGrid beingGrid;
    [SerializeField] private BeingSlot selectedSlot;

    bool canInteract = true;

    private void Start()
    {
        BeingBattleBus.FightStart += BeingBattleBus_BattleStart;
        BeingBattleBus.FightEnd += BeingBattleBus_BattleEnd;
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

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null) return;

            BeingSlot otherBeingSlot = hit.collider.gameObject.GetComponent<BeingSlot>();
            if (otherBeingSlot == null) return;
            if (!otherBeingSlot.isPlayerInteractable) return;

            if (!selectedSlot)
            {
                if (!otherBeingSlot.BeingController) return;
                selectedSlot = otherBeingSlot;
                Debug.Log("Selected!");
            }
            else
            {
                selectedSlot = null;
                Debug.Log("Swap!");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (selectedSlot == null) return;
            selectedSlot = null;
        }
    }
}