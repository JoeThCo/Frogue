using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BeingSO : ScriptableObject
{
    [Header("Values")]
    [SerializeField] private int startDamage;
    [SerializeField] private int startHealth;

    [Header("Ability")]
    [SerializeField] private Ability startAbility;

    [Header("Effects")]
    [SerializeField] Effect[] startEffects;

    [Header("Types")]
    [SerializeField] private BeingType[] startTypes;

    public int GetDamage() { return startDamage; }
    public int GetHealth() { return startHealth; }
    public BeingType[] GetTypes() { return startTypes; }
    public Effect[] GetEffects() { return startEffects; }
    public Ability GetAbility() { return startAbility; }
}