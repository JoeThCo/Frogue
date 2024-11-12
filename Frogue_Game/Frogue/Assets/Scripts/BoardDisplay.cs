using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardDisplay : MonoBehaviour
{
    [SerializeField] private float cellSize = 1.5f;
    [Space(10)]
    [SerializeField] private Transform slotParent;
    [SerializeField] private Transform beingParent;

    public Board Board { get; private set; }

    private BeingDisplay beingDisplayPrefab;
    private SlotDisplay slotDisplayPrefab;

    public void BoardDisplayInit(Board board)
    {
        this.Board = board;

        beingDisplayPrefab = Resources.Load<BeingDisplay>("BeingDisplay");
        slotDisplayPrefab = Resources.Load<SlotDisplay>("SlotDisplay");

        SpawnBeingDisplays(board);
        SpawnSlotDisplays();
    }

    private void SpawnSlotDisplays()
    {
        for (int x = 0; x < Board.BOARD_SIZE; x++)
        {
            for (int y = 0; y < Board.BOARD_SIZE; y++)
            {
                SlotDisplay slotDisplay = Instantiate(slotDisplayPrefab, Vector3.zero, Quaternion.identity, slotParent);
                slotDisplay.SlotDisplayInit(new Vector2Int(x, y));
                slotDisplay.transform.localPosition = new Vector3(x, 0, y) * cellSize;
            }
        }
    }

    private void SpawnBeingDisplays(Board board)
    {
        foreach (Being being in board)
        {
            if (being == null) continue;
            BeingDisplay beingDisplay = Instantiate(beingDisplayPrefab, Vector3.zero, Quaternion.identity, beingParent);
            beingDisplay.BeingDisplayInit(being);
            beingDisplay.transform.localPosition = new Vector3(being.Coords.x, 0, being.Coords.y) * cellSize;
        }
    }
}