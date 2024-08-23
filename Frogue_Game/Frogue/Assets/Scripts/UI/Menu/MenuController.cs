using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string startMenu;
    [SerializeField] private Menu[] allMenus;

    public event Action<Menu> OnMenuChange;

    public virtual void Start()
    {
        ShowMenu(startMenu);
    }

    public void ShowMenu(string menuName)
    {
        foreach (Menu menu in allMenus)
        {
            menu.gameObject.SetActive(menuName.Equals(menu.MenuName));
        }
    }
}