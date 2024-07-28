using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string startMenu;
    [SerializeField] private Menu[] allMenus;

    public static MenuController Instance;

    private void Start()
    {
        Instance = this;
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