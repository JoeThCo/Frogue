using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class BeingGrid : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Vector2Int gridSize = Vector2Int.one * 5;
    [SerializeField] private float cellSize = 2;

    [Header("Player")]
    [SerializeField] private bool isPlayerInteractable = false;

    [Header("Prefabs")]
    private BeingSlot beingSlotPrefab;
    private BeingController beingControllerPrefab;

    public int GridSpeed
    {
        get
        {
            int outputSpeed = 0;
            foreach (Being being in AliveBeings)
            {
                outputSpeed += being.Speed.GetFinalValue();
            }
            return outputSpeed;
        }
    }
    public bool HasAliveBeings
    {
        get
        {
            return AliveBeings.Length <= 0;
        }
    }
    public Being[] AliveBeings
    {
        get
        {
            UpdateBeings();
            return GetAliveBeings();
        }
    }

    private BeingSlot[,] allSlots;

    private void Start()
    {
        allSlots = new BeingSlot[gridSize.x, gridSize.y];

        beingSlotPrefab = ResourceManager.GetUI("BeingSlot").GetComponent<BeingSlot>();
        beingControllerPrefab = ResourceManager.GetUI("BeingController").GetComponent<BeingController>();

        MakeGrid();
        AddBeing(7);
    }

    public Being GetFirstBeing()
    {
        if (!HasAliveBeings)
            return AliveBeings[0];
        return null;
    }

    void UpdateBeings()
    {
        foreach (BeingSlot slot in allSlots)
        {
            if (!slot.BeingController) continue;
            if (!slot.BeingController.Being.Health.isDead()) continue;

            slot.BeingController = null;
        }
    }

    private Being[] GetAliveBeings()
    {
        List<Being> output = new List<Being>();

        foreach (BeingSlot slot in allSlots)
        {
            if (slot.BeingController)
            {
                output.Add(slot.BeingController.Being);
            }
        }

        return output.ToArray();
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
            if (!slot.BeingController)
            {
                BeingController beingController = Instantiate(beingControllerPrefab, slot.transform);
                beingController.BeingControllerInit(ResourceManager.GetBeing());
                slot.BeingController = beingController;
                return;
            }
        }
    }

    void AddBeing(int count)
    {
        for (int i = 0; i < count; i++)
            AddBeing();
    }

    public void SwapBeingControllers(BeingSlot selected, BeingSlot other)
    {
        if (other.BeingController == null)
        {
            selected.BeingController.transform.SetParent(other.transform);

            other.BeingController = selected.BeingController;
            selected.BeingController = null;

            other.BeingController.transform.localPosition = Vector2.zero;
        }
        else
        {
            BeingSlot tempSlot = selected;
            selected.BeingController.transform.SetParent(other.transform);
            other.BeingController.transform.SetParent(tempSlot.transform);

            BeingController tempController = selected.BeingController;
            selected.BeingController = other.BeingController;
            other.BeingController = tempController;

            selected.BeingController.transform.localPosition = Vector3.zero;
            other.BeingController.transform.localPosition = Vector3.zero;
        }
    }
}