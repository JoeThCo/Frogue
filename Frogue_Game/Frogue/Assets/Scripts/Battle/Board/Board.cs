using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : IEnumerable<Being>
{
    public const int BOARD_SIZE = 3;

    protected Being[,] board { get; set; }

    protected Vector2Int NO_SLOTS { get; set; } = -Vector2Int.one;

    public bool IsPlayerBoard { get; protected set; } = true;

    public Board()
    {
        board = new Being[BOARD_SIZE, BOARD_SIZE];
        Add(5);
    }

    public Board(Board board)
    {
        Update(board);
    }

    #region Board

    private void Add(int size)
    {
        for (int i = 0; i < size; i++)
            Add();
    }

    private void Add()
    {
        Vector2Int next = GetNextOpenSlot();

        if (next != NO_SLOTS)
        {
            Being being = new Being(next);
            board[next.x, next.y] = being;
        }
    }

    public void Update(Being[] beings)
    {
        board = new Being[BOARD_SIZE, BOARD_SIZE];
        foreach (Being being in beings)
            Set(new Being(being), being.Coords);
    }

    public void Update(Board _board)
    {
        board = new Being[BOARD_SIZE, BOARD_SIZE];
        foreach (Being being in _board)
            Set(new Being(being), being.Coords);
    }

    private void Set(Being being, Vector2Int coords)
    {
        board[coords.x, coords.y] = being;
    }

    private void Remove(BeingDisplay beingDisplay)
    {
        Set(null, beingDisplay.Coords);
    }

    public void Move(BeingDisplay beingDisplay, Vector2Int next)
    {
        Being being = board[beingDisplay.Coords.x, beingDisplay.Coords.y];
        if (being == null) return;

        being.Coords = next;
        board[next.x, next.y] = being;

        Remove(beingDisplay);
        beingDisplay.Coords = next;
    }

    public void Swap(BeingDisplay fromDisplay, BeingDisplay toDisplay)
    {
        Being fromBeing = board[fromDisplay.Coords.x, fromDisplay.Coords.y];
        Being toBeing = board[toDisplay.Coords.x, toDisplay.Coords.y];

        Vector2Int fromCoords = fromBeing.Coords;
        Vector2Int toCoords = toBeing.Coords;

        Set(null, fromCoords);
        Set(null, toCoords);

        fromBeing.Coords = toCoords;
        toBeing.Coords = fromCoords;

        Set(fromBeing, toCoords);
        Set(toBeing, fromCoords);
    }

    public virtual Vector2Int GetNextOpenSlot()
    {
        return NO_SLOTS;
    }

    public IEnumerator<Being> GetEnumerator()
    {
        foreach (Being being in board)
        {
            if (being != null)
                yield return being;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void PrintBoard()
    {
        foreach (Being being in board)
        {
            if (being != null)
                Debug.Log(being.ToString());
            else
                Debug.Log("");
        }
    }
    #endregion

    #region Battle
    public Being[] AliveBeings
    {
        get
        {
            List<Being> list = new List<Being>();
            foreach (Being being in board)
            {
                if (being != null && !being.Health.IsDead)
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
    #endregion
}