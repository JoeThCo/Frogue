using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Being Info", menuName = "ScriptableObjects/BeingInfo")]
public class BeingInit : ScriptableObject
{
    [SerializeField, Range(1, 10)] private int startHealth;
    [SerializeField, Range(1, 10)] private int startDamage;
    [SerializeField, Range(1, 10)] private int startTurnFrequency;

    public int GetDamage() { return startDamage; }
    public int GetHealth() { return startHealth; }
    public int GetTurnFrequency() { return startTurnFrequency; }
}