using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public Being[,] Beings { get; private set; }
    public bool IsPlayer { get; private set; }

    private Dictionary<Being, Vector2Int> beingsDictionary = new Dictionary<Being, Vector2Int>();

    public const int BOARD_SIZE = 3;

    public event Action<Being> BeingSpawned;
    public event Action<Vector2Int, Vector2Int> BeingMoved;
    public event Action<Vector2Int, Vector2Int> BeingsSwapped;
    public event Action BoardUpdated;

    public Board(bool isPlayer)
    {
        Beings = new Being[BOARD_SIZE, BOARD_SIZE];
        IsPlayer = isPlayer;

        BoardUpdated += Board_BoardUpdated;
    }

    public Board(Being[,] beings)
    {
        Beings = beings;
    }

    private void Board_BoardUpdated()
    {
        beingsDictionary = new Dictionary<Being, Vector2Int>();

        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                Being being = Beings[x, y];
                if (being != null) beingsDictionary.Add(being, new Vector2Int(x, y));
            }
        }
    }

    public Being Get(Vector2Int coords)
    {
        return Beings[coords.x, coords.y];
    }

    public void Swap(Vector2Int a, Vector2Int b)
    {
        (Beings[a.x, a.y], Beings[b.x, b.y]) = (Beings[b.x, b.y], Beings[a.x, a.y]);
        BeingsSwapped?.Invoke(a, b);
        BoardUpdated?.Invoke();
    }

    public BoardAction[] GetBoardAlignmentActions(Board targetBoard)
    {
        Board temp = new Board(Clone());

        List<BoardAction> actions = new List<BoardAction>();
        Dictionary<int, Vector2Int> basePositions = temp.GetBeingsDictionary();
        Dictionary<int, Vector2Int> targetPositions = targetBoard.GetBeingsDictionary();

        foreach (var being in basePositions.Keys)
        {
            Vector2Int currentPos = basePositions[being];
            Vector2Int targetPos = targetPositions[being];

            if (currentPos != targetPos)
            {
                Being targetBeing = temp.Get(targetPos);
                if (targetBeing != null) 
                    actions.Add(new Swap(currentPos, targetPos));
                else 
                    actions.Add(new Move(currentPos, targetPos));

                temp.Swap(currentPos, targetPos);
            }
        }

        return actions.ToArray();
    }

    private Dictionary<int, Vector2Int> GetBeingsDictionary()
    {
        Dictionary<int, Vector2Int> beingsDict = new Dictionary<int, Vector2Int>();

        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                Being being = Beings[x, y];
                if (being != null) beingsDict.Add(being.ID, new Vector2Int(x, y));
            }
        }

        return beingsDict;
    }


    public Vector2Int FindBeing(Being targetBeing)
    {
        if (beingsDictionary.TryGetValue(targetBeing, out Vector2Int coords))
        {
            return coords;
        }
        return new Vector2Int(-1, -1);
    }

    public void Move(Vector2Int oldCoords, Vector2Int newCoords)
    {
        Being being = Get(oldCoords);
        Get(oldCoords).Coords = newCoords;

        Set(null, oldCoords);
        Set(being, newCoords);

        BeingMoved?.Invoke(oldCoords, newCoords);
    }

    private void Set(Being value, Vector2Int coords)
    {
        Beings[coords.x, coords.y] = value;
        BoardUpdated?.Invoke();
    }

    public void Add()
    {
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                if (Beings[x, y] == null)
                {
                    Vector2Int coords = new Vector2Int(x, y);
                    Being being = new Being(coords);
                    Set(being, coords);
                    BeingSpawned?.Invoke(being);
                    return;
                }
            }
        }
    }

    public void Add(int total)
    {
        int count = Mathf.Min(BOARD_SIZE * BOARD_SIZE, total);
        for (int i = 0; i < count; i++)
            Add();
    }

    public Being[,] Clone()
    {
        Being[,] output = new Being[BOARD_SIZE, BOARD_SIZE];

        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                Being being = Get(new Vector2Int(x, y));
                if (being != null)
                {
                    output[x, y] = being.DeepClone();
                }
            }
        }

        return output;
    }

    public void Print()
    {
        Debug.LogWarning("Board");
        foreach (Being being in Beings)
        {
            if (being == null)
                Debug.Log("EMPTY");
            else
                Debug.Log(being.ToString());
        }
    }

    public Being[] AliveBeings
    {
        get
        {
            List<Being> list = new List<Being>();

            foreach (Being being in Beings)
            {
                if (being == null) continue;
                if (being.IsDead) continue;
                list.Add(being);
            }
            return list.ToArray();
        }
    }

    public Being Next
    {
        get
        {
            return AliveBeings[0];
        }
    }

    public bool IsDead
    {
        get
        {
            return AliveBeings.Length <= 0;
        }
    }
}