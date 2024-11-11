using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Board : IEnumerable<Being>
{
    public const int BOARD_SIZE = 3;

    private Being[,] board { get; set; }

    private Vector2Int NO_SLOTS { get; set; } = -Vector2Int.one;

    public Board()
    {
        board = new Being[BOARD_SIZE, BOARD_SIZE];

        Add(5);
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

    public Vector2Int GetNextOpenSlot()
    {
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                if (board[x, y] == null || board[x, y].IsDead)
                    return new Vector2Int(x, y);
            }
        }

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
}