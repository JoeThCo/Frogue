using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundEffectsManager
{
    [SerializeField] private static SoundEffect sfxPrefab;
    [SerializeField] private static AudioSO[] allSFXs;

    public static void Load()
    {
        sfxPrefab = Resources.Load<SoundEffect>("SFX/SFXPrefab");
        allSFXs = Resources.LoadAll<AudioSO>("SFX");
    }

    private static void SpawnSFX(string name, Vector2 position)
    {
        AudioSO audio = Helper.FindByName<AudioSO>(allSFXs, name);
        SoundEffect soundEffect = GameObject.Instantiate(sfxPrefab, position, Quaternion.identity).GetComponent<SoundEffect>();
        soundEffect.PlaySound(audio);
    }

    private static void SpawnSFX(Effect effect, Vector2 position)
    {
        SoundEffect soundEffect = GameObject.Instantiate(sfxPrefab, position, Quaternion.identity).GetComponent<SoundEffect>();
        soundEffect.PlaySound(effect.audioSO);
    }

    public static void PlaySFX(Effect effect, BeingController beingController)
    {
        SpawnSFX(effect, beingController.transform.position);
    }

    public static void PlaySFX(string name, BeingController beingController)
    {
        SpawnSFX(name, beingController.transform.position);
    }

    public static void PlaySFX(string name, BeingSlot beingSlot)
    {
        SpawnSFX(name, beingSlot.transform.position);
    }
}