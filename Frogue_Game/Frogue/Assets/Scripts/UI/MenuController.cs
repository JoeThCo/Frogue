using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string startMenu;

    private void Start()
    {
        DisplayMenu(startMenu);
    }

    public void DisplayMenu(string menuName)
    {
        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(menuName.Equals(transform.name));
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}