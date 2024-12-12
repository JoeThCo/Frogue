using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public int HP { get; private set; }
    public int MaxHP { get; private set; }
    public bool IsDead { get { return HP <= 0; } }

    public Health(BeingInit init)
    {
        this.HP = init.GetHealth();
        this.MaxHP = HP;
    }

    public Health(int hp)
    {
        this.HP = hp;
        this.MaxHP = hp;
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
    }
}