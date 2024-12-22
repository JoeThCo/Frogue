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

        Debug.LogWarning("Being Board");
        foreach (Being being in Board)
        {
            if (being == null) continue;
            Debug.Log(being.ToString());
        }
    }

    public Being Next
    {
        get
        {
            foreach (Being being in Board)
            {
                if (being == null) continue;
                if (being.Health.IsDead) continue;
                return being;
            }

            return null;
        }
    }

    public bool IsDead
    {
        get
        {
            foreach (Being being in Board)
                if (being != null && !being.Health.IsDead) return false;

            return true;
        }
    }
}