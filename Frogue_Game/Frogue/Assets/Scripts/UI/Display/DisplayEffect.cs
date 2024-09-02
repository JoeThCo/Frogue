using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEffect : MonoBehaviour
{
    public void Display(Effect[] effects)
    {
        foreach (Effect effect in effects)
        {
            Image image = Instantiate(ResourceManager.GetUI("WhoImage"), transform).GetComponent<Image>();
            image.sprite = effect.Icon;
            image.color = effect.Color;
        }
    }
}