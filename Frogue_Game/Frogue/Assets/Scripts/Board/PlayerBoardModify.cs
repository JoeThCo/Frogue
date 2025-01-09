using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBoardModify : MonoBehaviour
{
    public Board Board { get; private set; }
    public DisplayBoard DisplayBoard { get; private set; }

    private BeingDisplay SelectedBeingDisplay;

    public void PlayerBoardModifyInit(Board board, DisplayBoard displayBoard)
    {
        this.Board = board;
        this.DisplayBoard = displayBoard;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Board.Print();
        }

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GameManager.MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawLine(ray.origin, ray.direction * GameManager.MainCamera.farClipPlane, Color.white, 2.5f);
            if (!Physics.Raycast(ray, out hit)) return;

            BeingDisplay beingDisplay = hit.collider.gameObject.GetComponent<BeingDisplay>();
            SlotDisplay slotDisplay = hit.collider.gameObject.GetComponent<SlotDisplay>();
            if (beingDisplay != null && beingDisplay.CompareTag(GameManager.BADDIE_TAG)) return;
            if (slotDisplay != null && slotDisplay.CompareTag(GameManager.BADDIE_TAG)) return;

            if (SelectedBeingDisplay == null)
            {
                SelectedBeingDisplay = beingDisplay;
                Debug.Log(hit.collider.name);
            }
            else
            {
                if (beingDisplay != null && !beingDisplay.Equals(SelectedBeingDisplay))
                {
                    Debug.Log($"{Board.Get(SelectedBeingDisplay.Coords).ID} <==> {Board.Get(beingDisplay.Coords).ID}");
                    Board.Swap(SelectedBeingDisplay.Coords, beingDisplay.Coords);
                    SelectedBeingDisplay = null;
                }
                else if (slotDisplay != null)
                {
                    Debug.Log($"{Board.Get(SelectedBeingDisplay.Coords).ID} ==> {slotDisplay.Coords}");
                    Board.Move(SelectedBeingDisplay.Coords, slotDisplay.Coords);
                    SelectedBeingDisplay = null;
                }
            }
        }
    }
}