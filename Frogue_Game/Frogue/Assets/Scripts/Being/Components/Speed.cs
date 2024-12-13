using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed
{
    public int Turn { get; private set; }
    public readonly int TurnFrequency;

    public bool IsReady
    {
        get { return Turn >= TurnFrequency; }
    }

    public float TurnPercent
    {
        get
        {
            return 1 - ((float)Turn / (float)TurnFrequency);
        }
    }

    public int TurnsLeft
    {
        get
        {
            return TurnFrequency - Turn;
        }
    }

    public Speed(int turnFrequency)
    {
        this.Turn = 0;
        this.TurnFrequency = turnFrequency;
    }

    public Speed(int turn, int turnFrequency)
    {
        this.Turn = turn;
        this.TurnFrequency = turnFrequency;
    }

    public void OnTurn()
    {
        if (!IsReady)
            Turn++;
    }

    public void OnAttack()
    {
        Turn = 0;
    }
}