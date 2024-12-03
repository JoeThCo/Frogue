using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ResourceLoader
{
    private static Dictionary<string, BeingInfo> beingInfoDictionary;
    private static Dictionary<string, GameObject> gameDisplayDictionary;
    private static Dictionary<string, ParticleSystem> particleDictionary;
    private static Dictionary<string, AudioScriptableObject> soundEffectDictionary;
    private static Dictionary<string, GameObject> prefabDictionary;

    public static bool IsLoaded { get; private set; } = false;

    public static void Load()
    {
        if (IsLoaded) return;

        BeingInfo[] allBeingInfo = Resources.LoadAll<BeingInfo>("BeingInfo");
        beingInfoDictionary = allBeingInfo.ToDictionary(beingInfo => beingInfo.name, beingInfo => beingInfo);

        GameObject[] allGameObjects = Resources.LoadAll<GameObject>("GameDisplay");
        gameDisplayDictionary = allGameObjects.ToDictionary(gameObject => gameObject.name, gameObject => gameObject);

        ParticleSystem[] allParticles = Resources.LoadAll<ParticleSystem>("Particles");
        particleDictionary = allParticles.ToDictionary(particle => particle.name, particle => particle);

        AudioScriptableObject[] allSFX = Resources.LoadAll<AudioScriptableObject>("SFX");
        soundEffectDictionary = allSFX.ToDictionary(sfx => sfx.name, sfx => sfx);

        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("Prefabs");
        prefabDictionary = allPrefabs.ToDictionary(prefab => prefab.name, prefab => prefab);

        IsLoaded = true;
    }

    public static T GetByName<T>(string name, Dictionary<string, T> dictionary) where T : class
    {
        if (dictionary.TryGetValue(name, out T value))
        {
            return value;
        }

        Debug.LogWarning($"{typeof(T).Name} with name '{name}' not found.");
        return null;
    }

    private static T GetRandom<T>(T[] array) where T : ScriptableObject
    {
        if (array == null || array.Length == 0)
        {
            Debug.LogWarning("The provided array is null or empty.");
            return null;
        }

        int randomIndex = BattleManager.Random.Next(0, array.Length);
        return array[randomIndex];
    }

    public static BeingInfo GetBeingInfo() { return GetRandom<BeingInfo>(beingInfoDictionary.Values.ToArray()); }

    public static GameObject GetGameDisplay(string name) { return GetByName<GameObject>(name, gameDisplayDictionary); }

    #region Particles
    private static ParticleSystem GetParticle(string name) { return GetByName<ParticleSystem>(name, particleDictionary); }

    public static ParticleSystem SpawnParticle(string name, Vector3 spawnPosition)
    {
        ParticleSystem particleSystem = GameObject.Instantiate(GetParticle(name), spawnPosition, Quaternion.identity).GetComponent<ParticleSystem>();
        particleSystem.transform.position = spawnPosition;
        return particleSystem;
    }
    #endregion

    #region Prefab
    private static GameObject GetPrefab(string name) { return GetByName<GameObject>(name, prefabDictionary); }
    #endregion

    #region SFX
    private static AudioScriptableObject GetAudioScriptableObject(string name) { return GetByName<AudioScriptableObject>(name, soundEffectDictionary); }

    public static void SpawnSoundEffect(string name, Vector3 soundPosition)
    {
        SoundEffect soundEffect = GameObject.Instantiate(GetPrefab("SoundEffect"), soundPosition, Quaternion.identity).GetComponent<SoundEffect>();
        soundEffect.PlaySound(GetAudioScriptableObject(name));
    }

    public static void SpawnSoundEffect(string name)
    {
        SoundEffect soundEffect = GameObject.Instantiate(GetPrefab("SoundEffect"), Vector3.zero, Quaternion.identity).GetComponent<SoundEffect>();
        soundEffect.PlaySound(GetAudioScriptableObject(name));
    }
    #endregion
}