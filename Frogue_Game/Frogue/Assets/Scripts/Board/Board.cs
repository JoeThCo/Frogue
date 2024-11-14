using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : IEnumerable<Being>
{
    public const int BOARD_SIZE = 3;

    protected Being[,] board { get; set; }
    protected Vector2Int NO_SLOTS { get; set; } = -Vector2Int.one;

    public Board()
    {
        board = new Being[BOARD_SIZE, BOARD_SIZE];
        Add(5);
    }

    public Board DeepCopy()
    {
        Board newBoard = new Board();

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

    public void Add(int size)
    {
        for (int i = 0; i < size; i++)
            Add(new Being());
    }

    public void Add(Being being)
    {
        Vector2Int next = GetNextOpenSlot();
        if (next != NO_SLOTS)
        {
            being.SetCoords(next);
            board[next.x, next.y] = being;
        }
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
}