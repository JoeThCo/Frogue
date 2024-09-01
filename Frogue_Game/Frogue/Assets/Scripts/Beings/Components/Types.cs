using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Types
{
    public List<BeingType> BeingTypes { get; private set; } = new List<BeingType>();

    public Types(BeingType[] types)
    {
        BeingTypes.AddRange(types);
    }

    public bool IsBeingOfTypes(List<BeingType> beingTypes) 
    {
        foreach(BeingType type in beingTypes) 
        {
            if (IsBeingOfType(type))
                return true;
        }

        return false;
    }

    private bool IsBeingOfType(BeingType type)
    {
        return BeingTypes.Contains(type);
    }

    public Color GetMainColor()
    {
        return BeingTypes[0].Color;
    }

    public Sprite GetIcon() 
    {
        return BeingTypes[0].Icon;
    }

    public override string ToString()
    {
        string output = string.Empty;

        foreach (BeingType type in BeingTypes)
        {
            output += type.ToString() + " ";
        }

        return output;
    }
}