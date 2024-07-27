using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingHolder : MonoBehaviour
{
    [SerializeField] protected Vector2Int gridSize = Vector2Int.one * 5;
    [SerializeField] private int BeingsToSpawn = 5;

    public Being[] AliveBeings
    {
        get
        {
            UpdateBeings();
            return GetAliveBeings();
        }
    }

    private BeingSlot[,] allSlots;

    private BeingSlot beingSlotPrefab;
    private Being beingControllerPrefab;

    public virtual void Start()
    {
        allSlots = new BeingSlot[gridSize.x, gridSize.y];

        beingSlotPrefab = ResourceManager.GetUI("BeingSlot").GetComponent<BeingSlot>();
        beingControllerPrefab = ResourceManager.GetUI("Being").GetComponent<Being>();

        MakeGrid();
        AddBeing(BeingsToSpawn);
    }

    public int GetHolderSpeed()
    {
        int outputSpeed = 0;
        foreach (Being being in AliveBeings)
        {
            outputSpeed += being.Speed.GetFinalValue();
        }
        return outputSpeed;
    }

    public bool IsDead()
    {
        return AliveBeings.Length <= 0;
    }

    public Being GetNext()
    {
        return AliveBeings[0];
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
            if (slot.Being && slot.Being.Health.isDead())
            {
                Destroy(slot.Being.gameObject);
                slot.Being = null;
            }
        }
    }

    private Being[] GetAliveBeings()
    {
        List<Being> output = new List<Being>();

        foreach (BeingSlot slot in allSlots)
        {
            if (slot.Being && !slot.Being.Health.isDead())
                output.Add(slot.Being);
        }

        return output.ToArray();
    }

    private bool AddBeing()
    {
        foreach (BeingSlot slot in allSlots)
        {
            if (!slot.Being)
            {
                Being being = Instantiate(beingControllerPrefab, slot.transform);
                being.BeingInit(ResourceManager.GetBeing());
                slot.Being = being;
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