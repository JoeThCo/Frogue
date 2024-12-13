using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField, Range(0, BOARD_SIZE * BOARD_SIZE)] private int BeingsToSpawn;
    [Space(10)]
    [SerializeField] protected float cellSize = 1.5f;
    [Space(10)]
    [SerializeField] private Transform slotParent;
    [SerializeField] private Transform beingParent;

    protected BeingDisplay[,] displayBoard;

    private BeingDisplay beingDisplayPrefab;
    private SlotDisplay slotDisplayPrefab;

    public const int BOARD_SIZE = 3;

    public virtual void BoardInit()
    {
        beingDisplayPrefab = ResourceLoader.GetGameDisplay("BeingDisplay").GetComponent<BeingDisplay>();
        slotDisplayPrefab = ResourceLoader.GetGameDisplay("SlotDisplay").GetComponent<SlotDisplay>();

        displayBoard = new BeingDisplay[BOARD_SIZE, BOARD_SIZE];
    }

    #region Spawning
    protected void SpawnSlotDisplays(bool isPlayerInteractable)
    {
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                SlotDisplay slotDisplay = Instantiate(slotDisplayPrefab, Vector3.zero, Quaternion.identity, slotParent);
                slotDisplay.SlotDisplayInit(new Vector2Int(x, y), isPlayerInteractable);
                slotDisplay.transform.localPosition = new Vector3(x, 0, y) * cellSize;
            }
        }
    }

    protected void SpawnBeingDisplays(bool isPlayerInteractable)
    {
        int count = 0;
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                if (count >= BeingsToSpawn) return;

                BeingDisplay beingDisplay = SpawnNewBeingDisplay(new Being(new Vector2Int(x, y)), isPlayerInteractable);
                ModifyBeingDisplay(beingDisplay);
                displayBoard[x, y] = beingDisplay;
                count++;
            }
        }
    }

    private BeingDisplay SpawnNewBeingDisplay(Being being, bool isPlayerInteractable)
    {
        BeingDisplay beingDisplay = Instantiate(beingDisplayPrefab, Vector3.zero, Quaternion.identity, beingParent);
        beingDisplay.BeingDisplayInit(being, isPlayerInteractable);
        return beingDisplay;
    }

    protected virtual void ModifyBeingDisplay(BeingDisplay beingDisplay)
    {
        beingDisplay.transform.localPosition = new Vector3(beingDisplay.Being.Coords.x, 0, beingDisplay.Being.Coords.y) * cellSize;
    }
    #endregion

    public void Swap(BeingDisplay a, BeingDisplay b)
    {
        Vector2Int aCoords = a.Being.Coords;
        Vector2Int bCoords = b.Being.Coords;

        a.Being.Coords = bCoords;
        b.Being.Coords = aCoords;

        displayBoard[aCoords.x, aCoords.y] = b;
        displayBoard[bCoords.x, bCoords.y] = a;
    }

    public void Move(BeingDisplay a, Vector2Int coords)
    {
        Vector2Int tempCoords = a.Being.Coords;

        a.Being.Coords = coords;

        displayBoard[coords.x, coords.y] = a;
        displayBoard[tempCoords.x, tempCoords.y] = null;
    }

    public void PrintBoard()
    {
        foreach (BeingDisplay beingDisplay in displayBoard)
        {
            if (beingDisplay != null)
                Debug.Log(beingDisplay);
        }
    }
}