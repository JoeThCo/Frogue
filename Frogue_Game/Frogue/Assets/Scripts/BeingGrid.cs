using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class BeingGrid : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Vector2Int gridSize = Vector2Int.one * 5;
    [SerializeField] private float cellSize = 2;

    [Header("Player")]
    [SerializeField] private bool isPlayerInteractable = false;

    [Header("Prefabs")]
    [SerializeField] private BeingSlot beingSlotPrefab;
    [SerializeField] private Being beingPrefab;

    private BeingSlot[,] allSlots;
    public Being[] AliveBeings { get; private set; }

    private void Start()
    {
        BeingBattleBus.BattleStart += BeingBattleBus_BattleStart;
        allSlots = new BeingSlot[gridSize.x, gridSize.y];

        MakeGrid();
        AddBeing(7);
    }

    private void BeingBattleBus_BattleStart()
    {
        AliveBeings = GetAliveBeings();
    }

    public Being GetFirstBeing()
    {
        if (isBeingsLeft())
            return AliveBeings[0];
        return null;
    }

    bool isBeingsLeft()
    {
        return AliveBeings.Length > 0;
    }

    public Being[] GetAliveBeings()
    {
        List<Being> output = new List<Being>();

        foreach (BeingSlot slot in allSlots)
        {
            if (slot.Being)
            {
                output.Add(slot.Being);
            }
        }

        return output.ToArray();
    }

    BeingSlot GetBeingSlot(Vector2Int coords)
    {
        return allSlots[coords.x, coords.y];
    }

    void MakeGrid()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                Vector2Int coords = new Vector2Int(y, x);
                AddBeingSlot(coords);
            }
        }
    }

    Vector2 GetBeingSlotLocalPosition(BeingSlot beingSlot)
    {
        Vector2 position = ((Vector2)beingSlot.Coords * cellSize) - (Vector2.one * gridSize) + ((Vector2.up + Vector2.right) * gridSize * 0.5f);
        if (beingSlot.Coords.y % 2 == 0)
            return position + (Vector2.left * cellSize * 0.5f);
        return position;
    }

    void AddBeingSlot(Vector2Int coords)
    {
        BeingSlot beingSlot = Instantiate(beingSlotPrefab, transform);
        beingSlot.BeingSlotInit(coords, isPlayerInteractable);
        beingSlot.transform.localPosition = GetBeingSlotLocalPosition(beingSlot);

        allSlots[coords.y, coords.x] = beingSlot;
    }

    void AddBeing()
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

    void AddBeing(int count)
    {
        for (int i = 0; i < count; i++)
            AddBeing();
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

    public BeingSlot[] GetNeighbors(BeingSlot slot)
    {
        List<BeingSlot> output = new List<BeingSlot>();
        List<Vector2Int> neighbors = new List<Vector2Int> { Vector2Int.up, Vector2Int.one, Vector2Int.left, Vector2Int.right, Vector2Int.down, Vector2Int.left + Vector2Int.up };

        foreach (Vector2Int neighborCoord in neighbors)
        {
            Vector2Int current = slot.Coords + neighborCoord;
            BeingSlot currentSlot = GetBeingSlot(current);

            if (IsValid(current) && !output.Contains(currentSlot))
                output.Add(currentSlot);
        }

        return output.ToArray();
    }

    bool IsValid(Vector2Int coords)
    {
        return coords.x >= 0 && coords.x < gridSize.x && coords.y >= 0 && coords.y < gridSize.y;
    }

    void PrintGrid()
    {
        foreach (BeingSlot slot in allSlots)
        {
            if (slot.Being)
            {
                Debug.Log(slot.Being.ToString());
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
            PrintGrid();
    }
}