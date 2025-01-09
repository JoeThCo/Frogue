using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Being
{
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int ID { get; private set; }
    public Vector2Int Coords { get; set; }
    public bool IsDead => Health <= 0;

    public event Action<Being> OnDead;

    public Being(Vector2Int coords)
    {
        ID = Random.Range(-100, 100);
        Health = Random.Range(5, 10);
        Attack = Random.Range(5, 10);

        this.Coords = coords;
    }

    private Being(int health, int attack, int iD, Vector2Int coords)
    {
        Health = health;
        Attack = attack;
        ID = iD;
        Coords = coords;
    }

    public Being DeepClone()
    {
        // Since Health, Attack, and ID are ints
        // and Coords is a struct (Vector2Int),
        // they are all copied by value.
        // Thus, this is effectively a deep copy.
        return new Being(this.Health, this.Attack, this.ID, this.Coords);
    }

    public void GetDamaged(Being target)
    {
        Health -= target.Attack;

        if (IsDead)
            OnDead?.Invoke(this);
    }

    public override string ToString()
    {
        return $"[ID:{ID}, Health:{Health} Attack:{Attack}] @ {Coords}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Being other = obj as Being;
        return other.Health == Health &&
            other.Attack == Attack &&
            other.ID == ID;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}