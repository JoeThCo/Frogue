using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : IEnumerable<Being>
{
    public const int BOARD_SIZE = 3;

    protected Being[,] board { get; set; }
    protected Vector2Int NO_SLOTS { get; set; } = -Vector2Int.one;

    public Being Next
    {
        get
        {
            return AliveBeings[0];
        }
    }

    public Being[] AliveBeings
    {
        get
        {
            List<Being> aliveBeings = new List<Being>();

            foreach (Being current in board)
                if (current != null && !current.BeingInfo.IsDead)
                    aliveBeings.Add(current);

            return aliveBeings.ToArray();
        }
    }

    public bool IsDead
    {
        get
        {
            return AliveBeings.Length <= 0;
        }
    }

    public Board()
    {
        board = new Being[BOARD_SIZE, BOARD_SIZE];
        Add(5);
    }

    #region Board

    private Being this[int x, int y]
    {
        get => board[x, y];
        set => board[x, y] = value;
    }

    public T GetBeingSnapshot<T>() where T : Board, new()
    {
        T newBoard = new T();
        newBoard.board = new Being[BOARD_SIZE, BOARD_SIZE];

        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                Being being = board[x, y];
                if (being != null)
                    newBoard.board[x, y] = being.MakeSnapshot();
            }
        }

        return newBoard;
    }

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

    public void Remove(Being being)
    {
        board[being.BeingInfo.Coords.x, being.BeingInfo.Coords.y] = null;
    }

    public void Move(BeingDisplay beingDisplay, Vector2Int next)
    {
        Being being = board[beingDisplay.Coords.x, beingDisplay.Coords.y];
        if (being == null) return;

        being.BeingInfo.UpdateCoords(next);
        board[next.x, next.y] = being;

        board[beingDisplay.Coords.x, beingDisplay.Coords.y] = null;
        beingDisplay.UpdateCoords(next);
    }

    public void Swap(BeingDisplay fromDisplay, BeingDisplay toDisplay)
    {
        Being fromBeing = board[fromDisplay.Coords.x, fromDisplay.Coords.y];
        Being toBeing = board[toDisplay.Coords.x, toDisplay.Coords.y];
        if (fromBeing == null || toBeing == null) return;

        Vector2Int fromCoords = fromBeing.BeingInfo.Coords;
        Vector2Int toCoords = toBeing.BeingInfo.Coords;

        board[fromCoords.x, fromCoords.y] = null;
        board[toCoords.x, toCoords.y] = null;

        fromBeing.BeingInfo.UpdateCoords(toCoords);
        toBeing.BeingInfo.UpdateCoords(fromCoords);

        board[toCoords.x, toCoords.y] = fromBeing;
        board[fromCoords.x, fromCoords.y] = toBeing;
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
    public void UpdateBoard(Board board)
    {
        for (int y = 0; y < BOARD_SIZE; y++)
            for (int x = 0; x < BOARD_SIZE; x++)
                this.board[x, y] = board[x, y];
    }
    #endregion
}