using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    [SerializeField] private Trigger trigger;
    [SerializeField] private Effect effect;
    [SerializeField] private EffectWho effectWho;
}