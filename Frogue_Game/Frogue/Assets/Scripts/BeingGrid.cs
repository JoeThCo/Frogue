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
        addBeing();
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

    void setBeingSlotPosition(BeingSlot beingSlot)
    {
        Vector2 position = ((Vector2)beingSlot.coords * cellSize) - (Vector2.one * gridSize) + ((Vector2.up + Vector2.right) * gridSize * 0.5f);
        beingSlot.transform.localPosition = position;

        if (beingSlot.coords.y % 2 == 0)
            beingSlot.transform.localPosition = (Vector2)beingSlot.transform.localPosition + (Vector2.left * cellSize * 0.5f);
    }

    void addBeingSlot(Vector2Int coords)
    {
        BeingSlot beingSlot = Instantiate(beingSlotPrefab, transform);
        beingSlot.BeingSlotInit(coords);
        setBeingSlotPosition(beingSlot);

        allSlots[coords.x, coords.y] = beingSlot;
    }

    void addBeing()
    {
        foreach (BeingSlot slot in allSlots)
        {
            if (slot.Being == null)
            {
                Being being = Instantiate(beingPrefab, slot.transform);
                being.BeingInit();
                slot.Being = being;
                return;
            }
        }
    }
}