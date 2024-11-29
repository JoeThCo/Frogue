using System.Collections;
using System.Collections.Generic;
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
                if (current != null && !current.IsDead)
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

    #region DeepCopy
    public PlayerBoard PlayerDeepCopy()
    {
        PlayerBoard newBoard = new PlayerBoard();

        newBoard.board = new Being[BOARD_SIZE, BOARD_SIZE];

        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                Being being = board[x, y];
                if (being != null)
                {
                    newBoard.board[x, y] = being.DeepCopy();
                }
            }
        }

        return newBoard;
    }

    public BaddieBoard BaddieDeepCopy()
    {
        BaddieBoard newBoard = new BaddieBoard();

        newBoard.board = new Being[BOARD_SIZE, BOARD_SIZE];

        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                Being being = board[x, y];
                if (being != null)
                {
                    newBoard.board[x, y] = being.DeepCopy();
                }
            }
        }

        return newBoard;
    }
    #endregion

    private void Add(int size)
    {
        for (int i = 0; i < size; i++)
            Add(new Being());
    }

    private void Add(Being being)
    {
        Vector2Int next = GetNextOpenSlot();
        if (next != NO_SLOTS)
        {
            being.SetCoords(next);
            board[next.x, next.y] = being;
        }
    }

    public void Remove(Being being)
    {
        board[being.Coords.x, being.Coords.y] = null;
    }

    public void Move(Being being, Vector2Int next)
    {
        if (being == null) return;

        being.SetCoords(next);
        board[next.x, next.y] = being;
    }

    public void Swap(Being a, Being b)
    {
        if (a == null || b == null) return;

        Vector2Int aCoords = a.Coords;
        Vector2Int bCoords = b.Coords;

        board[aCoords.x, aCoords.y] = null;
        board[bCoords.x, bCoords.y] = null;

        a.SetCoords(bCoords);
        b.SetCoords(aCoords);

        board[bCoords.x, bCoords.y] = a;
        board[aCoords.x, aCoords.y] = b;
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