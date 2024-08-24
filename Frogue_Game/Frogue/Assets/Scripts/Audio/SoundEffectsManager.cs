using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    [SerializeField] private static SoundEffect sfxPrefab;
    [SerializeField] private static AudioSO[] allSFXs;

    public static void Load()
    {
        sfxPrefab = Resources.Load<SoundEffect>("SFX/SFXPrefab");
        allSFXs = Resources.LoadAll<AudioSO>("SFX");

        foreach (AudioSO s in allSFXs)
            Debug.Log(s.name);
    }

    public static void PlaySFX(string name, Being being)
    {
        AudioSO audio = Helper.FindByName<AudioSO>(allSFXs, name);
        SoundEffect soundEffect = Instantiate(sfxPrefab, being.transform.position, Quaternion.identity).GetComponent<SoundEffect>();
        soundEffect.PlaySound(audio);
    }
}