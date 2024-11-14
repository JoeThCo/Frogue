using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardDisplay : MonoBehaviour
{
    [SerializeField] protected float cellSize = 1.5f;
    [Space(10)]
    [SerializeField] private Transform slotParent;
    [SerializeField] private Transform beingParent;

    public Board Board { get; private set; }

    private BeingDisplay beingDisplayPrefab;
    private SlotDisplay slotDisplayPrefab;

    public virtual void BoardDisplayInit(Board board)
    {
        this.Board = board;

        beingDisplayPrefab = Resources.Load<BeingDisplay>("BeingDisplay");
        slotDisplayPrefab = Resources.Load<SlotDisplay>("SlotDisplay");
    }

    protected void SpawnSlotDisplays(bool isPlayerInteractable)
    {
        for (int x = 0; x < Board.BOARD_SIZE; x++)
        {
            for (int y = 0; y < Board.BOARD_SIZE; y++)
            {
                SlotDisplay slotDisplay = Instantiate(slotDisplayPrefab, Vector3.zero, Quaternion.identity, slotParent);
                slotDisplay.SlotDisplayInit(new Vector2Int(x, y), isPlayerInteractable);
                slotDisplay.transform.localPosition = new Vector3(x, 0, y) * cellSize;
            }
        }
    }

    private BeingDisplay SpawnBeingDisplay(Being being, bool isPlayerInteractable)
    {
        BeingDisplay beingDisplay = Instantiate(beingDisplayPrefab, Vector3.zero, Quaternion.identity, beingParent);
        beingDisplay.BeingDisplayInit(being, isPlayerInteractable);
        return beingDisplay;
    }

    protected virtual void ModifyBeingDisplay(BeingDisplay beingDisplay)
    {
        beingDisplay.transform.localPosition = new Vector3(beingDisplay.Being.Coords.x, 0, beingDisplay.Being.Coords.y) * cellSize;
    }

    protected void SpawnBeingDisplays(Board board, bool isPlayerInteractable)
    {
        foreach (Being being in board)
        {
            if (being == null) continue;
            BeingDisplay beingDisplay = SpawnBeingDisplay(being, isPlayerInteractable);
            ModifyBeingDisplay(beingDisplay);
        }
    }
}