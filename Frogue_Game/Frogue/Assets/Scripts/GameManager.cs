using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPlaying { get; private set; } = false;

    private void Start()
    {
        ResourceManager.LoadResources();
    }
}