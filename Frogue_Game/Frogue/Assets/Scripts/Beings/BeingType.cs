using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type", menuName = "ScriptableObject/Type")]
public class BeingType : ScriptableObject
{
    [SerializeField] private Color _color = Color.white;
    public Color Color { get; private set; }

    public void TypeInit()
    {
        this.Color = _color;
    }
}