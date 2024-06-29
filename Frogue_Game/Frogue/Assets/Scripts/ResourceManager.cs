using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private static Trigger[] allTriggers;
    private static Who[] allWho;
    private static Effect[] allEffects;

    public static void ResourceInit()
    {
        allTriggers = Resources.LoadAll<Trigger>("Trigger");
        allWho = Resources.LoadAll<Who>("Who");
        allEffects = Resources.LoadAll<Effect>("Effect");
    }
}