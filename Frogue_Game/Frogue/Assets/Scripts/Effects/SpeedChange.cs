using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speed Change", menuName = "ScriptableObject/Effect/SpeedChange")]
public class SpeedChange : Effect
{
    [SerializeField][Range(-5, 5)] private int ChangeAmount = 0;

    public override int GetChange()
    {
        return ChangeAmount;
    }
}