using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool UseSeed = true;
    [SerializeField] private int UserSeed;

    public static int Seed;
    public static System.Random Random;
    public bool isPlaying { get; private set; } = false;

    private void Start()
    {
        if (UseSeed)
            Seed = UserSeed;
        else
            Seed = Guid.NewGuid().GetHashCode();

        Random = new System.Random(Seed);
        Debug.LogFormat("Seed: {0}", Seed);

        ResourceManager.Load();
        SoundEffectsManager.Load();
    }
}