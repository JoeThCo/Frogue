using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayType : MonoBehaviour
{
    public void Display(ConditionWho conditionWho)
    {
        TypeWho typeWho = conditionWho as TypeWho;
        if (typeWho == null) return;

        foreach (BeingType type in typeWho.BeingTypes)
        {
            Image image = Instantiate(ResourceManager.GetUI("WhoImage"), transform).GetComponent<Image>();
            image.sprite = type.Icon;
        }
    }
}