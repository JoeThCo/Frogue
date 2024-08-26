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

    public void TakeDamage(Being other)
    {
        int finalDamage = other.Damage.GetFinalValue();
        if (finalDamage <= 0) return;

        HPLeft -= finalDamage;
        SpawnDamageText(other, finalDamage);
        SoundEffectsManager.PlaySFX("Damage", other);

        OnHealthChanged?.Invoke(this);
        Debug.LogFormat($"-{finalDamage} | {HPLeft} / {MaxHP}");

        if (isDead())
            OnDeath?.Invoke();
    }

    public void HealBeing(Healing healing)
    {
        int finalHealing = healing.HealingAmount;
        if (finalHealing <= 0) return;

        HPLeft += finalHealing;
        HPLeft = Math.Min(HPLeft, MaxHP);
        SoundEffectsManager.PlaySFX("Healing", being);

        OnHealthChanged?.Invoke(this);
        Debug.LogFormat($"+{finalHealing} | {HPLeft} / {MaxHP}");
    }

    public void ChangeMaxHealth(MaxHealthChange maxHealthChange)
    {

    }

    void SpawnDamageText(Being being, int finalDamage)
    {
        DamageTextPopup damageText = GameObject.Instantiate(ResourceManager.GetUI("DamageTextPopUp")).GetComponent<DamageTextPopup>();
        damageText.DamageTextPopUpInit(being, finalDamage);
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