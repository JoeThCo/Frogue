using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BeingSO : ScriptableObject
{
    public int startDamage;
    public int startHealth;
    public Effect[] startEffects;
    public BeingType[] startTypes;
}