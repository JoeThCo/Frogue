using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class BoardDisplay : MonoBehaviour
{
    public Board Board { get; private set; }

    private BeingDisplay beingDisplayPrefab;
    private SlotDisplay slotDisplayPrefab;

    public void BoardDisplayInit(Board board)
    {
        this.Board = board;

        beingDisplayPrefab = Resources.Load<BeingDisplay>("BeingDisplay");
        slotDisplayPrefab = Resources.Load<SlotDisplay>("SlotDisplay");

        SpawnDisplaySlotDisplays(board);
        SpawnDisplaySlot();
    }

    private void SpawnDisplaySlot()
    {
        for (int x = 0; x < Board.BOARD_SIZE; x++)
        {
            for (int y = 0; y < Board.BOARD_SIZE; y++)
            {
                SlotDisplay slotDisplay = Instantiate(slotDisplayPrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
            }
        }
    }

    private void SpawnDisplaySlotDisplays(Board board)
    {
        foreach (Being being in board)
        {
            if (being == null) continue;
            Debug.Log(being);
            BeingDisplay beingDisplay = Instantiate(beingDisplayPrefab, new Vector3(being.Coords.x, 0, being.Coords.y), Quaternion.identity, transform);
        }
    }
}