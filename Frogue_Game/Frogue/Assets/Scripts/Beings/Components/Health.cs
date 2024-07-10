using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public int HPLeft { get; private set; }
    public int MaxHP { get; private set; }


    public Health(int startHealth)
    {
        HPLeft = startHealth;
        MaxHP = HPLeft;
    }

    public void TakeDamage(Damage damage)
    {
        HPLeft -= damage.GetFinalValue();
        Debug.LogFormat("-{0} | {1} / {2}", damage.GetFinalValue(), HPLeft, MaxHP);

        if (HPLeft <= 0)
        {
            OnDeath();
        }
    }

    public bool isDead() { return HPLeft <= 0; }

    void OnDeath()
    {
        Debug.Log("Dead!");
    }
}