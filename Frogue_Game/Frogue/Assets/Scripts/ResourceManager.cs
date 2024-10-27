using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private static BeingSO[] allFrogs;
    private static BeingSO[] allBaddies;

    private static Effect[] allEffects;
    private static GameObject[] allUI;

    private static ParticleSystem[] allVFX;

    public static bool isLoaded { get; private set; } = false;

    public static void Load()
    {
        if (isLoaded) return;

        allVFX = Resources.LoadAll<ParticleSystem>("VFX");
        allEffects = Resources.LoadAll<Effect>("Effect");
        allUI = Resources.LoadAll<GameObject>("UI");

        allBaddies = Resources.LoadAll<BeingSO>("Baddie");
        allFrogs = Resources.LoadAll<BeingSO>("Frog");

        isLoaded = true;
    }

    public static BeingSO GetFrog()
    {
        return Helper.GetRandom<BeingSO>(allFrogs);
    }

    public static BeingSO GetBaddie()
    {
        return Helper.GetRandom<BeingSO>(allBaddies);
    }

    public static Effect GetEffect()
    {
        return Helper.GetRandom<Effect>(allEffects);
    }

    public static GameObject GetUI(string name)
    {
        return Helper.FindByName<GameObject>(allUI, name);
    }

    public static ParticleSystem GetVFX(Effect effect)
    {
        return Helper.FindByName<ParticleSystem>(allVFX, effect.name);
    }
}