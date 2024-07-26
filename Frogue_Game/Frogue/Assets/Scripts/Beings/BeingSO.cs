using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Being", menuName = "ScriptableObject/Being")]
public class BeingSO : ScriptableObject
{
    [SerializeField][Range(1, 10)] public int startDamage;
    [SerializeField][Range(1, 10)] public int startHealth;
    [SerializeField][Range(1, 10)] public int startSpeed;
    [SerializeField] public Effect[] startEffects;
    [SerializeField] public BeingType[] startTypes;
}