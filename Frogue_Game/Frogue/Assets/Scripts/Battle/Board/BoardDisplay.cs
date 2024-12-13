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

    protected BeingDisplay[,] displayBoard;

    private BeingDisplay beingDisplayPrefab;
    private SlotDisplay slotDisplayPrefab;

    public const int BOARD_SIZE = 3;

    public virtual void BoardDisplayInit()
    {
        beingDisplayPrefab = ResourceLoader.GetGameDisplay("BeingDisplay").GetComponent<BeingDisplay>();
        slotDisplayPrefab = ResourceLoader.GetGameDisplay("SlotDisplay").GetComponent<SlotDisplay>();

        displayBoard = new BeingDisplay[BOARD_SIZE, BOARD_SIZE];
    }

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

    protected void SpawnBeingDisplay(bool isPlayerInteractable)
    {
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                BeingDisplay beingDisplay = SpawnNewBeingDisplay(new Being(new Vector2Int(x, y)), isPlayerInteractable);
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
        beingDisplay.transform.localPosition = new Vector3(beingDisplay.Coords.x, 0, beingDisplay.Coords.y) * cellSize;
    }
}