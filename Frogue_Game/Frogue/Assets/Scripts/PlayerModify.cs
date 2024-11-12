using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModify : MonoBehaviour
{
    private Camera cam;
    private Board playerBoard;

    private BeingDisplay selectedBeingDisplay;

    private void Start()
    {
        cam = Camera.main;
    }

    public void PlayerModifyInit(Board playerBoard)
    {
        this.playerBoard = playerBoard;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawLine(ray.origin, ray.direction * 10);

            if (Physics.Raycast(ray, out hit))
            {
                BeingDisplay beingDisplay = hit.collider.gameObject.GetComponent<BeingDisplay>();
                SlotDisplay slotDisplay = hit.collider.gameObject.GetComponent<SlotDisplay>();

                if (selectedBeingDisplay != null)
                {
                    float moveTime = 0.25f;

                    if (beingDisplay != null && beingDisplay != selectedBeingDisplay)
                    {
                        Vector3 tempPos = beingDisplay.transform.position;
                        beingDisplay.transform.DOMove(selectedBeingDisplay.transform.position, moveTime);
                        selectedBeingDisplay.transform.DOMove(tempPos, moveTime);
                    }

                    if (slotDisplay != null)
                    {
                        Vector3 newPosition = new Vector3(slotDisplay.transform.position.x, selectedBeingDisplay.transform.position.y, slotDisplay.transform.position.z);
                        selectedBeingDisplay.transform.DOMove(newPosition, moveTime);
                    }

                    selectedBeingDisplay = null;
                }
                else
                {
                    selectedBeingDisplay = beingDisplay;
                }
            }
        }
    }
}