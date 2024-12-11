using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModify : MonoBehaviour
{
    private Camera Cam;
    private Board playerBoard;

    private BeingDisplay selectedBeingDisplay;

    public static event Action<BeingDisplay> SwapBeingsDisplay;
    public static event Action<SlotDisplay> MoveBeingDisplay;

    public static event Action<BeingDisplay> SelectBeingDisplay;
    public static event Action<BeingDisplay> UnSelectBeingDisplay;

    public void PlayerModifyInit(Board playerBoard)
    {
        this.playerBoard = playerBoard;

        Cam = Camera.main;

        SwapBeingsDisplay += PlayerModify_SwapBeings;
        MoveBeingDisplay += PlayerModify_MoveBeing;

        SelectBeingDisplay += PlayerModify_SelectBeingDisplay;
        UnSelectBeingDisplay += PlayerModify_UnSelectBeingDisplay;
    }

    private void OnDisable()
    {
        SwapBeingsDisplay -= PlayerModify_SwapBeings;
        MoveBeingDisplay -= PlayerModify_MoveBeing;

        SelectBeingDisplay -= PlayerModify_SelectBeingDisplay;
        UnSelectBeingDisplay -= PlayerModify_UnSelectBeingDisplay;
    }

    private void PlayerModify_SelectBeingDisplay(BeingDisplay beingDisplay)
    {
        selectedBeingDisplay = beingDisplay;
    }

    private void PlayerModify_UnSelectBeingDisplay(BeingDisplay obj)
    {
        selectedBeingDisplay = null;
    }


    private void PlayerModify_MoveBeing(SlotDisplay slotDisplay)
    {
        Vector3 newPosition = new Vector3(slotDisplay.transform.position.x, selectedBeingDisplay.transform.position.y, slotDisplay.transform.position.z);

        selectedBeingDisplay.Move(newPosition);
        playerBoard.Move(selectedBeingDisplay, slotDisplay.Coords);
    }

    private void PlayerModify_SwapBeings(BeingDisplay beingDisplay)
    {
        Vector3 tempPos = beingDisplay.transform.position;

        beingDisplay.Move(selectedBeingDisplay.transform.position);
        selectedBeingDisplay.Move(tempPos);

        playerBoard.Swap(beingDisplay, selectedBeingDisplay);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawLine(ray.origin, ray.direction * 25);

            if (Physics.Raycast(ray, out hit))
            {
                BeingDisplay beingDisplay = hit.collider.gameObject.GetComponent<BeingDisplay>();
                SlotDisplay slotDisplay = hit.collider.gameObject.GetComponent<SlotDisplay>();

                if (beingDisplay != null && !beingDisplay.IsPlayerInteractable) return;
                if (slotDisplay != null && !slotDisplay.IsPlayerInteractable) return;

                if (selectedBeingDisplay != null)
                {
                    //swapping two beings
                    if (beingDisplay != null && beingDisplay != selectedBeingDisplay)
                        SwapBeingsDisplay?.Invoke(beingDisplay);

                    //moving being to a slot
                    if (slotDisplay != null)
                        MoveBeingDisplay?.Invoke(slotDisplay);

                    UnSelectBeingDisplay?.Invoke(selectedBeingDisplay);
                }
                else
                {
                    SelectBeingDisplay?.Invoke(beingDisplay);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (selectedBeingDisplay == null) return;
            UnSelectBeingDisplay?.Invoke(selectedBeingDisplay);
        }
    }
}