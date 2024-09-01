using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCondition : MonoBehaviour
{
    [SerializeField] DisplayType displayTypePrefab;

    public void Display(ConditionWho conditionWho)
    {
        if (conditionWho is TypeWho)
        {
            TypeWho typeWho = conditionWho as TypeWho;
            DisplayType displayType = Instantiate(displayTypePrefab, gameObject.transform);
            displayType.Display(typeWho);
        }
    }
}