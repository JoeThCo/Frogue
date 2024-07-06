using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Types
{
    public List<BeingType> BeingTypes { get; private set; } = new List<BeingType>();
    public bool IsBeingTypes
    {
        get
        {
            return BeingTypes.Count > 0;
        }
    }

    public void TypesInit()
    {
        AddRandomType();
    }

    void AddRandomType()
    {
        BeingType randomBeingType = ResourceManager.GetBeingType();

        if (!BeingTypes.Contains(randomBeingType))
        {
            BeingTypes.Add(randomBeingType);
        }
    }

    public Color GetMainColor()
    {
        if (IsBeingTypes)
        {
            return BeingTypes[0].Color;
        }

        return Color.black;
    }
}