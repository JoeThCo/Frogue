using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ScriptableObject
{
    public virtual int GetChange()
    {
        return 0;
    }
}