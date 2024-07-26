using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Types
{
    public List<BeingType> BeingTypes { get; private set; } = new List<BeingType>();
    public bool HasBeingTypes
    {
        get
        {
            return BeingTypes.Count > 0;
        }
    }

    public Types(BeingType[] types)
    {
        foreach (BeingType type in types) 
        {
            type.TypeInit();
        }

        BeingTypes.AddRange(types);
    }

    public Color GetMainColor()
    {
        if (HasBeingTypes)
        {
            return BeingTypes[0].Color;
        }

        return Color.white;
    }
}