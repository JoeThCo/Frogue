using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health
{
    public int HPLeft { get; private set; }
    public int MaxHP { get; private set; }

    public event Action OnDeath;
    public event Action<Health> OnHealthChanged;

    public Health(int startHealth)
    {
        HPLeft = startHealth;
        MaxHP = HPLeft;
    }

    public void TakeDamage(Being other)
    {
        int finalDamage = other.Damage.GetFinalValue();
        if (finalDamage <= 0) return;

        HPLeft -= finalDamage;
        SpawnDamageText(other, finalDamage);
        OnHealthChanged?.Invoke(this);
        Debug.LogFormat($"-{finalDamage} | {HPLeft} / {MaxHP}");

        if (isDead())
            OnDeath?.Invoke();
    }

    void SpawnDamageText(Being being, int finalDamage)
    {
        DamageTextPopup damageText = GameObject.Instantiate(ResourceManager.GetUI("DamageTextPopUp")).GetComponent<DamageTextPopup>();
        damageText.DamageTextPopUpInit(being, finalDamage);
    }

    public float GetPercent() { return (float)HPLeft / (float)MaxHP; }
    public string GetHealthText() { return $"{HPLeft} / {MaxHP}"; }
    public bool isDead() { return HPLeft <= 0; }
}