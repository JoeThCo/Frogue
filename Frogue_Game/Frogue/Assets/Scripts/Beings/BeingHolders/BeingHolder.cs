using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BeingHolder : MonoBehaviour
{
    [SerializeField] protected Vector2Int gridSize = Vector2Int.one * Helper.GRID_SIZE;
    [SerializeField] private float cellSize = 2;
    [Space(10)]
    [SerializeField] private int BeingsToSpawn = 5;
    [Space(10)]
    [SerializeField] private bool isPlayerInteractable = false;

    public BeingSlot[,] AllSlots;

    private BeingSlot beingSlotPrefab;
    private Being beingControllerPrefab;

    public virtual void Start()
    {
        AllSlots = new BeingSlot[Helper.GRID_SIZE, Helper.GRID_SIZE];

        beingSlotPrefab = ResourceManager.GetUI("BeingSlot").GetComponent<BeingSlot>();
        beingControllerPrefab = ResourceManager.GetUI("Being").GetComponent<Being>();

        MakeGrid();
        AddBeing(BeingsToSpawn);
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

    void AddBeingSlot(Vector2Int coords)
    {
        BeingSlot beingSlot = Instantiate(beingSlotPrefab, transform);
        beingSlot.BeingSlotInit(coords, isPlayerInteractable);
        beingSlot.transform.localPosition = GetBeingSlotPosition(beingSlot);

        AllSlots[coords.y, coords.x] = beingSlot;
    }

    private bool AddBeing()
    {
        int index = 0;

        for (int x = gridSize.x - 1; x >= 0; x--)
        {
            for (int y = gridSize.y - 1; y >= 0; y--)
            {
                BeingSlot slot = AllSlots[y, x];
                if (!slot.Being)
                {
                    Being being = Instantiate(beingControllerPrefab, slot.transform);

                    if (isPlayerInteractable)
                    {
                        being.BeingInit(ResourceManager.GetFrog());
                        being.gameObject.name = $"Frog #{index}";
                    }
                    else
                    {
                        being.BeingInit(ResourceManager.GetBaddie());
                        being.gameObject.name = $"Baddie #{index}";
                    }

                    slot.Being = being;
                    //Debug.Log($"{x}, {y}");
                    return true;
                }
                index++;
            }
        }
        return false;
    }

    protected void AddBeing(int count)
    {
        for (int i = 0; i < count; i++)
            AddBeing();
    }

    public bool IsDead()
    {
        return GetAliveBeings().Length <= 0;
    }

    public Being GetNext()
    {
        return GetAliveBeings()[0];
    }

    public Being[] GetBeingsToAttack(Who who)
    {
        return who.GetWho(this);
    }

    public Vector3 GetBeingSlotPosition(BeingSlot beingSlot)
    {
        Vector2 position = ((Vector2)beingSlot.Coords * cellSize) - (Vector2.one * gridSize.x) + ((Vector2.up + Vector2.right) * Helper.GRID_SIZE * 0.5f);
        if (beingSlot.Coords.y % 2 == 0)
            return position + (Vector2.left * cellSize * 0.5f);
        return position;
    }

    public Being[] GetAliveBeings()
    {
        List<Being> output = new List<Being>();

        foreach (BeingSlot slot in GetAliveBeingSlots().OrderBy(b => b.Coords.x).ThenBy(b => b.Coords.y))
            output.Add(slot.Being);

        return output.ToArray();
    }

    public BeingSlot[] GetAliveBeingSlots()
    {
        List<BeingSlot> output = new List<BeingSlot>();

        foreach (BeingSlot slot in AllSlots)
            if (slot && slot.Being && !slot.Being.Health.isDead())
                output.Add(slot);

        return output.ToArray();
    }

    public void DisplaySelectionSlots(BeingSlot beingslot)
    {
        bool[,] filledIn = LocationWho.GetFilledDots(beingslot.Being.BeingInfo.GetAbility());

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (filledIn[x, y])
                    AllSlots[x, y].OnSelect();
                else
                    AllSlots[x, y].OnDeselect();
            }
        }
    }
}