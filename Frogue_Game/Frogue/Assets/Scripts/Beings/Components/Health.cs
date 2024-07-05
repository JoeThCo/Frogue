using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HPLeft { get; private set; }
    public int MaxHP { get; private set; }

    public void HealthInit()
    {
        HPLeft = Random.Range(5, 11);
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