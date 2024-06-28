using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectWho", menuName = "ScriptableObjects/EffectWho")]
public class EffectWho : ScriptableObject
{
    public Vector2[] GetEffected()
    {
        return new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    }
}