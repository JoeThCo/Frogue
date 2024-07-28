using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health
{
    public int HPLeft { get; private set; }
    public int MaxHP { get; private set; }

    public event Action OnDeath;

    public Health(int startHealth)
    {
        HPLeft = startHealth;
        MaxHP = HPLeft;
    }

    public void TakeDamage(Being being)
    {
        int finalDamage = being.Damage.GetFinalValue();
        HPLeft -= finalDamage;
        SpawnDamageText(being, finalDamage);

        Debug.LogFormat("-{0} | {1} / {2}", being.Damage.GetFinalValue(), HPLeft, MaxHP);

        if (isDead())
            OnDeath?.Invoke();
    }

    void SpawnDamageText(Being being, int finalDamage)
    {
        DamageTextPopup damageText = GameObject.Instantiate(ResourceManager.GetUI("DamageTextPopUp")).GetComponent<DamageTextPopup>();
        damageText.DamageTextPopUpInit(being, finalDamage);
    }

    public bool isDead() { return HPLeft <= 0; }
}