using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string startMenu;
    [SerializeField] private Menu[] allMenus;

    public virtual void Start()
    {
        MenusInit();
        ShowMenu(startMenu);
    }

    public void ShowMenu(string menuName)
    {
        foreach (Menu menu in allMenus)
        {
            menu.menuParent.gameObject.SetActive(menuName.Equals(menu.MenuName));
        }
    }

    private void MenusInit()
    {
        foreach (Menu menu in allMenus)
        {
            menu.MenuInit();
        }
    }
}