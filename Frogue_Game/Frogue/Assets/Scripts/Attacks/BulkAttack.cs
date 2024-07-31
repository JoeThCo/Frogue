using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObject/BulkAttack")]
public class BulkAttack : ScriptableObject
{
    [SerializeField] private Who WhoToAttack;

    public Being[] GetBeingsToAttack(BeingHolder beingHolder)
    {
        return WhoToAttack.GetWho(beingHolder);
    }
}