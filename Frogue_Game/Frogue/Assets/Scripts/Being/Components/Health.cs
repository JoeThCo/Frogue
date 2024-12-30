using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public int HP { get; private set; }
    public readonly int MaxHP;
    public bool IsDead { get { return HP <= 0; } }

    public Health(int hp)
    {
        this.HP = hp;
        this.MaxHP = hp;
    }

    public Health(int hp, int maxHp) 
    {
        this.HP = hp;
        this.MaxHP = maxHp;
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
    }
}