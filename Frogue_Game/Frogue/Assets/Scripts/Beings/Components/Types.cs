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

    public Types(BeingType[] types)
    {
        BeingTypes.AddRange(types);
    }

    public Color GetMainColor()
    {
        if (IsBeingTypes)
        {
            return BeingTypes[0].Color;
        }

        return Color.white;
    }
}