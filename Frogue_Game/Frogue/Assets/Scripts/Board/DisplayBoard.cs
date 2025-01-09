using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DisplayBoard : MonoBehaviour
{
    [SerializeField] private bool IsPlayerInteractable = true;
    [SerializeField] private float cellSize = 1.5f;

    private SlotDisplay slotDisplayPrefab;
    private BeingDisplay beingDisplayPrefab;

    private Board board;

    public SlotDisplay[,] SlotDisplays { get; private set; }

    public void DisplayBoardInit(Board board)
    {
        this.board = board;
        SlotDisplays = new SlotDisplay[Board.BOARD_SIZE, Board.BOARD_SIZE];

        slotDisplayPrefab = Resources.Load<SlotDisplay>("SlotDisplay");
        beingDisplayPrefab = Resources.Load<BeingDisplay>("BeingDisplay");

        SpawnDisplaySlots();

        this.board.BeingSpawned += Board_BeingSpawned;
        this.board.BeingsSwapped += Board_BeingsSwapped;
        this.board.BeingMoved += Board_BeingMoved;
    }

    public IEnumerator DisplaySwaps(Board input)
    {
        BoardAction[] boardActions = board.GetBoardAlignmentActions(input);
        Debug.Log(boardActions.Length);

        foreach (BoardAction action in boardActions)
        {
            if (action is Move)
            {
                board.Move(action.From, action.To);
            }
            else if (action is Swap)
            {
                board.Swap(action.From, action.To);
            }

            yield return new WaitForSeconds(.5f);
        }
    }

    private SlotDisplay Get(Vector2Int a)
    {
        return SlotDisplays[a.x, a.y];
    }

    private void Board_BeingMoved(Vector2Int oldCoords, Vector2Int newCoords)
    {
        SlotDisplay oldSlot = Get(oldCoords);
        SlotDisplay newSlot = Get(newCoords);

        if (oldSlot.BeingDisplay == null) return;
        oldSlot.BeingDisplay.transform.SetParent(null);

        newSlot.BeingDisplay = oldSlot.BeingDisplay;
        oldSlot.BeingDisplay = null;

        newSlot.BeingDisplay.transform.SetParent(newSlot.BeingOffset);
        newSlot.BeingDisplay.transform.DOLocalMove(Vector3.zero, 0.25f).SetEase(Ease.InOutQuad);

        newSlot.BeingDisplay.Coords = newCoords;
    }

    private void Board_BeingsSwapped(Vector2Int a, Vector2Int b)
    {
        SlotDisplay aSlot = Get(a);
        SlotDisplay bSlot = Get(b);

        aSlot.BeingDisplay.transform.SetParent(null);
        bSlot.BeingDisplay.transform.SetParent(null);

        // Swap BeingDisplays
        (aSlot.BeingDisplay, bSlot.BeingDisplay) = (bSlot.BeingDisplay, aSlot.BeingDisplay);

        aSlot.BeingDisplay.transform.SetParent(aSlot.BeingOffset);
        bSlot.BeingDisplay.transform.SetParent(bSlot.BeingOffset);

        aSlot.BeingDisplay.transform.DOLocalMove(Vector3.zero, 0.25f).SetEase(Ease.InOutQuad);
        bSlot.BeingDisplay.transform.DOLocalMove(Vector3.zero, 0.25f).SetEase(Ease.InOutQuad);

        aSlot.BeingDisplay.Coords = a;
        bSlot.BeingDisplay.Coords = b;
    }

    private void Board_BeingSpawned(Being being)
    {
        SlotDisplay slotDisplay = SlotDisplays[being.Coords.x, being.Coords.y];
        BeingDisplay beingDisplay = Instantiate(beingDisplayPrefab, Vector3.zero, Quaternion.identity, slotDisplay.BeingOffset);

        beingDisplay.transform.localPosition = Vector3.zero;
        beingDisplay.transform.localRotation = IsPlayerInteractable ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

        beingDisplay.BeingDisplayInit(being, IsPlayerInteractable);
        slotDisplay.BeingDisplay = beingDisplay;
    }

    private void SpawnDisplaySlots()
    {
        for (int x = 0; x < Board.BOARD_SIZE; x++)
        {
            for (int y = 0; y < Board.BOARD_SIZE; y++)
            {
                SlotDisplay slotDisplay = Instantiate(slotDisplayPrefab, Vector3.zero, Quaternion.identity, transform);
                slotDisplay.transform.localPosition = new Vector3(x, 0, y) * cellSize;
                slotDisplay.SlotDisplayInit(new Vector2Int(x, y), IsPlayerInteractable);
                SlotDisplays[x, y] = slotDisplay;
            }
        }
    }
}