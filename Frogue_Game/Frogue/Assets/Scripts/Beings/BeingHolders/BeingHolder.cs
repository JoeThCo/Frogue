using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingHolder : MonoBehaviour
{
    [SerializeField] protected Vector2Int gridSize = Vector2Int.one * 5;
    [SerializeField] private int BeingsToSpawn = 5;

    public BeingSlot[] AliveBeings
    {
        get
        {
            UpdateBeings();
            return GetAliveBeings();
        }
    }
    public bool HasAliveBeings
    {
        get
        {
            return AliveBeings.Length <= 0;
        }
    }
    public int TotalSpeed
    {
        get
        {
            int outputSpeed = 0;
            foreach (BeingSlot beingSlot in AliveBeings)
            {
                outputSpeed += beingSlot.BeingController.Being.Speed.GetFinalValue();
            }
            return outputSpeed;
        }
    }

    private BeingSlot[,] allSlots;

    private BeingSlot beingSlotPrefab;
    private BeingController beingControllerPrefab;

    public virtual void Start()
    {
        allSlots = new BeingSlot[gridSize.x, gridSize.y];

        beingSlotPrefab = ResourceManager.GetUI("BeingSlot").GetComponent<BeingSlot>();
        beingControllerPrefab = ResourceManager.GetUI("BeingController").GetComponent<BeingController>();

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
        beingSlot.BeingSlotInit(coords);
        beingSlot.transform.localPosition = GetBeingSlotPosition(beingSlot);

        allSlots[coords.y, coords.x] = beingSlot;
    }

    public virtual Vector3 GetBeingSlotPosition(BeingSlot beingSlot)
    {
        return Vector3.zero;
    }

    private void UpdateBeings()
    {
        foreach (BeingSlot slot in allSlots)
        {
            if (!slot.BeingController) continue;
            if (!slot.BeingController.Being.Health.isDead()) continue;

            slot.BeingController = null;
        }
    }

    private BeingSlot[] GetAliveBeings()
    {
        List<BeingSlot> output = new List<BeingSlot>();

        foreach (BeingSlot slot in allSlots)
        {
            if (slot.BeingController)
            {
                output.Add(slot);
            }
        }

        return output.ToArray();
    }

    private bool AddBeing()
    {
        foreach (BeingSlot slot in allSlots)
        {
            if (!slot.BeingController)
            {
                BeingController beingController = Instantiate(beingControllerPrefab, slot.transform);
                beingController.BeingControllerInit(ResourceManager.GetBeing());
                slot.BeingController = beingController;
                return true;
            }
        }
        return false;
    }

    protected void AddBeing(int count)
    {
        for (int i = 0; i < count; i++)
            AddBeing();
    }
}