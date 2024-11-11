using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public const int BOARD_SIZE = 3;

    Being[,] board;

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
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                if (board[x, y] == null) continue;
                if (!board[x, y].IsDead) continue;
                being.SetCoords(new Vector2Int(x, y));
                board[x, y] = being;
            }
        }
    }
}