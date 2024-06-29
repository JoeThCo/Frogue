using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Who", menuName = "ScriptableObjects/Who")]
public class Who : ScriptableObject
{
    public Vector2[] GetWho()
    {
        return new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    }

    public override string ToString()
    {
        return name;
    }
}