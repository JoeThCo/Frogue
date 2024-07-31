using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Change", menuName = "ScriptableObject/Effect/DamageChange")]
public class DamageChange : Effect
{
    [SerializeField][Range(-5, 5)] private int ChangeAmount = 0;

    public override int GetChange()
    {
        return ChangeAmount;
    }
}