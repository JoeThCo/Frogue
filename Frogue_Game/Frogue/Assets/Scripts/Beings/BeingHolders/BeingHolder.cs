using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingHolder : MonoBehaviour
{
    [SerializeField] protected Vector2Int gridSize = Vector2Int.one * Helper.GRID_SIZE;
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
        foreach (BeingSlot slot in AllSlots)
        {
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
                return true;
            }
            index++;
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

    public virtual Vector3 GetBeingSlotPosition(BeingSlot beingSlot)
    {
        return Vector3.zero;
    }

    public Being[] GetAliveBeings()
    {
        List<Being> output = new List<Being>();

        foreach (BeingSlot slot in GetAliveBeingSlots())
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
}