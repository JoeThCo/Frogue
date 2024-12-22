using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeingDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHP;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI textAttack;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI textTurn;
    [SerializeField] private Image imageSpeedFill;

    public void BeingDisplayUIInit(Being being)
    {
        textHP.SetText(being.Health.HP.ToString());
        textAttack.SetText(being.Attack.ToString());

        textTurn.SetText(being.Speed.TurnsLeft.ToString());
        imageSpeedFill.fillAmount = being.Speed.TurnPercent;
    }

    public void OnDamage(DamageAction damageAction)
    {
        textHP.SetText(damageAction.HealthOutput.ToString());
    }
}