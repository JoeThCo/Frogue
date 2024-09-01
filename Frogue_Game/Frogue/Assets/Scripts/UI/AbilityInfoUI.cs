using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI abilityName;
    [SerializeField] private TextMeshProUGUI abilityDescription;

    public void AbilityInfoInit(Ability ability)
    {
        abilityName.SetText(ability.name);
    }
}