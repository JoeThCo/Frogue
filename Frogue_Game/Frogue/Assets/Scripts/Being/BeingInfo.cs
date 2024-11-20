using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Being Info", menuName = "ScriptableObjects/BeingInfo")]
public class BeingInfo : ScriptableObject
{
    [SerializeField] private int startHealth;
    [SerializeField] private int startDamage;

    public int GetDamage() { return startDamage; }
    public int GetHealth() { return startHealth; }
}