using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingBoard
{
    public bool IsPlayer { get; private set; }
    public Being[,] Board { get; set; }

    public BeingBoard(BeingDisplay[,] displays, bool isPlayer)
    {
        Board = new Being[DisplayBoard.BOARD_SIZE, DisplayBoard.BOARD_SIZE];
        IsPlayer = isPlayer;

        foreach (BeingDisplay display in displays)
        {
            if (display == null) continue;
            if (display.Being == null) continue;

            Board[display.Being.Coords.x, display.Being.Coords.y] = new Being(display.Being);
        }
    }

    public BeingBoard(BeingDisplay[,] displays) 
    {
        Board = new Being[DisplayBoard.BOARD_SIZE, DisplayBoard.BOARD_SIZE];
        IsPlayer = false;

        List<Vector2Int> coordsToPickFrom = GetAllBoardCoords();

        foreach (BeingDisplay display in displays)
        {
            if (display == null) continue;
            if (display.Being == null) continue;

            Vector2Int coords = coordsToPickFrom[Random.Range(0, coordsToPickFrom.Count)];
            Board[coords.x, coords.y] = new Being(display.Being);
            coordsToPickFrom.Remove(coords);
        }
    }

    private List<Vector2Int> GetAllBoardCoords()
    {
        List<Vector2Int> coords = new List<Vector2Int>();

        for (int x = 0; x < DisplayBoard.BOARD_SIZE; x++)
        {
            for (int y = 0; y < DisplayBoard.BOARD_SIZE; y++)
            {
                coords.Add(new Vector2Int(x, y));
            }
        }

        return coords;
    }


    public Being[] AliveBeings
    {
        get
        {
            List<Being> list = new List<Being>();
            foreach (Being being in Board)
            {
                if (being == null) continue;
                if (being.Health.IsDead) continue;
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