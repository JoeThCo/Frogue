using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabbedMenu : MonoBehaviour
{
    [SerializeField] private MenuController menuController;

    public void ShowMenu(string menuName)
    {
        menuController.ShowMenu(menuName);
    }
}