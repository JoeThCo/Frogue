using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeingType", menuName = "ScriptableObject/BeingType")]
public class BeingType : ScriptableObject
{
    public Color Color = Color.white;
    public Sprite Icon;

    public override string ToString()
    {
        return name;
    }
}