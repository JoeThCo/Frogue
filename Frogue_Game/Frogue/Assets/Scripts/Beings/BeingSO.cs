using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BeingSO : ScriptableObject
{
    [SerializeField] private int startDamage;
    [SerializeField] private int startHealth;
    [SerializeField] Effect[] startEffects;
    [SerializeField] private BeingType[] startTypes;

    public int GetDamage() { return startDamage; }
    public int GetHealth() { return startHealth; }
    public BeingType[] GetTypes() { return startTypes; }
    public Effect[] GetEffects() { return startEffects; }
}