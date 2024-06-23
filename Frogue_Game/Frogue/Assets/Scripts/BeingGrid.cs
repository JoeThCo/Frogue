using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class BeingGrid : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Vector2Int gridSize = Vector2Int.one * 5;
    [SerializeField] private float cellSize = 2;

    [Header("Prefabs")]
    [SerializeField] private BeingSlot beingSlotPrefab;
    [SerializeField] private Being beingPrefab;

    private BeingSlot[,] allSlots;

    private void Start()
    {
        allSlots = new BeingSlot[gridSize.x, gridSize.y];

        makeGrid();
        addBeing(5);
    }

    void makeGrid()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                Vector2Int coords = new Vector2Int(x, y);
                addBeingSlot(coords);
            }
        }
    }

    Vector2 getBeingSlotLocalPosition(BeingSlot beingSlot)
    {
        Vector2 position = ((Vector2)beingSlot.Coords * cellSize) - (Vector2.one * gridSize) + ((Vector2.up + Vector2.right) * gridSize * 0.5f);
        if (beingSlot.Coords.y % 2 == 0)
            return position + (Vector2.left * cellSize * 0.5f);
        return position;
    }

    void addBeingSlot(Vector2Int coords)
    {
        BeingSlot beingSlot = Instantiate(beingSlotPrefab, transform);
        beingSlot.BeingSlotInit(coords);
        beingSlot.transform.localPosition = getBeingSlotLocalPosition(beingSlot);

        allSlots[coords.x, coords.y] = beingSlot;
    }

    void addBeing()
    {
        foreach (BeingSlot slot in allSlots)
        {
            if (!slot.Being)
            {
                Being being = Instantiate(beingPrefab, slot.transform);
                being.BeingInit();
                slot.Being = being;
                return;
            }
        }
    }

    void addBeing(int count)
    {
        for (int i = 0; i < count; i++)
        {
            addBeing();
        }
    }

    public void SwapBeings(BeingSlot selected, BeingSlot other)
    {
        if (other.Being == null)
        {
            selected.Being.transform.SetParent(other.transform);

            other.Being = selected.Being;
            selected.Being = null;

            other.Being.transform.localPosition = Vector2.zero;
        }
        else
        {
            BeingSlot tempSlot = selected;
            selected.Being.transform.SetParent(other.transform);
            other.Being.transform.SetParent(tempSlot.transform);

            Being temp = selected.Being;
            selected.Being = other.Being;
            other.Being = temp;

            selected.Being.transform.localPosition = Vector2.zero;
            other.Being.transform.localPosition = Vector2.zero;
        }
    }
}