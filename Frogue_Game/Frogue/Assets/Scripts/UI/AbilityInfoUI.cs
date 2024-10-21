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
    [SerializeField] private DisplayCondition displayConditionInfo;
    [SerializeField] private DisplayEffect displayEffect;

    public void AbilityInfoInit(BeingSlot beingSlot)
    {
        if (beingSlot.Being == null) return;

        Ability ability = beingSlot.Being.BeingInfo.GetAbility();
        if (ability == null) return;

        ClearInfo();
        AddInfo(ability);
    }

    void ClearInfo()
    {
        foreach (Transform t in displayLocation.transform)
            Destroy(t.gameObject);

        foreach (Transform t in displayConditionInfo.transform)
            Destroy(t.gameObject);

        foreach (Transform t in displayEffect.transform)
            Destroy(t.gameObject);
    }

    void AddInfo(Ability ability)
    {
        abilityName.SetText(ability.name);
        displayLocation.Display(ability);

        foreach (ConditionWho conditionWho in ability.ConditionWhos)
        {
            DisplayCondition displayCondition = Instantiate(displayConditionPrefab, displayConditionInfo.transform).GetComponent<DisplayCondition>();
            displayCondition.Display(conditionWho);
        }

        displayEffect.Display(ability.EffectsToApply);
    }
}