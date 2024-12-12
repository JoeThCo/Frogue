using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeingDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI attackText;

    private BeingDisplay BeingDisplay;

    public void BeingDisplayUIInit(Being being, BeingDisplay beingDisplay)
    {
        this.BeingDisplay = beingDisplay;

        BeingDisplay.BeingDisplayDamaged += BeingDisplay_BeingDisplayDamaged;
        
        hpText.SetText(being.Health.HP.ToString());
        attackText.SetText(being.Attack.ToString());
    }

    private void BeingDisplay_BeingDisplayDamaged(DamageAction damageAction)
    {
        hpText.SetText(damageAction.FinalHealth.ToString());
    }
}