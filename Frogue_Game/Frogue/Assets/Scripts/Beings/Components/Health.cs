using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public int HPLeft { get; private set; }
    public int MaxHP { get; private set; }

    public Health()
    {
        HPLeft = 5;
        MaxHP = HPLeft;
    }

    public Health(int hp)
    {
        HPLeft = hp;
        MaxHP = HPLeft;
    }

    public void TakeDamage(Damage damage)
    {
        HPLeft -= damage.DamageAmount;

        if (HPLeft <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        Debug.Log("Dead!");
    }
}