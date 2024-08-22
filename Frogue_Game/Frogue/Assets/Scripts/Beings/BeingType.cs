using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeingType", menuName = "ScriptableObject/BeingType")]
public class BeingType : ScriptableObject
{
    [SerializeField] private Color _color = Color.white;
    public Color Color { get; private set; }

    public void TypeInit()
    {
        this.Color = _color;
    }

    public override string ToString()
    {
        return name;
    }
}