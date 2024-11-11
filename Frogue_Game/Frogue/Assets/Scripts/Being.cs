using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being
{
    public int HP { get; private set; }
    public int Attack { get; private set; }
    public Vector2Int Coords { get; private set; }

    public bool IsDead { get { return HP <= 0; } }

    public Being()
    {
        HP = Random.Range(1, 11);
        Attack = Random.Range(1, 4);
    }

    public Being(int hp, int attack)
    {
        this.HP = hp;
        this.Attack = attack;
    }

    public Being Clone()
    {
        return MemberwiseClone() as Being;
    }

    public void SetCoords(Vector2Int coords)
    {
        this.Coords = coords;
    }
}