using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenuController : MenuController
{
    public static SceneMenuController Instance;

    public override void Start()
    {
        base.Start();

        if (Instance == null)
            Instance = this;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}