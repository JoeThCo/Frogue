using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string MenuName;
    [SerializeField] public Transform menuParent;

    public virtual void MenuInit() { }
}
