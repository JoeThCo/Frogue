using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMenuController : MenuController
{
    public static SceneMenuController Instance;

    public override void Start()
    {
        base.Start();

        if (Instance == null)
            Instance = this;
    }
}