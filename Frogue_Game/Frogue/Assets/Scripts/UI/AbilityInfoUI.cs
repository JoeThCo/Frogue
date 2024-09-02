using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class AbilityInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI abilityName;

    [Header("Prefabs")]
    [SerializeField] private DisplayCondition displayConditionPrefab;

    [Header("Parent")]
    [SerializeField] private DisplayLocation displayLocation;
    [SerializeField] private HorizontalLayoutGroup conditionInfo;

    public void AbilityInfoInit(BeingSlot beingSlot)
    {
        if (beingSlot.Being == null) return;
        Ability ability = beingSlot.Being.BeingInfo.GetAbility();

        foreach (Transform t in displayLocation.transform)
            Destroy(t.gameObject);

        foreach (Transform t in conditionInfo.transform)
            Destroy(t.gameObject);

        abilityName.SetText(ability.name);
        displayLocation.Display(ability);

        foreach (ConditionWho conditionWho in ability.ConditionWhos)
        {
            DisplayCondition displayCondition = Instantiate(displayConditionPrefab, conditionInfo.transform).GetComponent<DisplayCondition>();
            displayCondition.Display(conditionWho);
        }
    }
}