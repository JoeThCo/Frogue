using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : IFinalValue
{
    public int HPLeft { get; private set; }
    public int MaxHP { get; private set; }

    public event Action OnDeath;
    public event Action<Health> OnHealthChanged;

    private Being being;

    public Health(Being being, int startHealth)
    {
        this.being = being;
        HPLeft = startHealth;
        MaxHP = HPLeft;
    }

    public int TakeDamage(Being other)
    {
        int finalDamage = other.Damage.GetFinalValue();
        if (finalDamage <= 0) return -1;

        HPLeft -= finalDamage;

        OnHealthChanged?.Invoke(this);
        Debug.LogFormat($"-{finalDamage} HP | {HPLeft} / {MaxHP}");

        if (isDead())
            OnDeath?.Invoke();

        return finalDamage;
    }

    public void HealBeing(Healing healing)
    {
        if (HPLeft >= MaxHP) return;

        int finalHealing = healing.HealingAmount;
        if (finalHealing <= 0) return;

        HPLeft += finalHealing;
        HPLeft = Math.Min(HPLeft, MaxHP);

        OnHealthChanged?.Invoke(this);
        Debug.LogFormat($"+{finalHealing} HP | {HPLeft} / {MaxHP}");
    }

    public void ChangeMaxHealth(MaxHealthChange maxHealthChange)
    {
        int finalMaxHealthChange = maxHealthChange.HealthChange;
        HPLeft += finalMaxHealthChange;
        MaxHP += finalMaxHealthChange;

        OnHealthChanged?.Invoke(this);
        Debug.LogFormat($"+{finalMaxHealthChange} MAX HP | {HPLeft} / {MaxHP}");
    }

    public float GetPercent() { return (float)HPLeft / (float)MaxHP; }
    public bool isDead() { return HPLeft <= 0; }

    public override string ToString()
    {
        return $"{HPLeft}/{MaxHP}";
    }

    public int GetFinalValue()
    {
        return being.Effects.GetFinalValue(HPLeft, typeof(Healing));
    }
}