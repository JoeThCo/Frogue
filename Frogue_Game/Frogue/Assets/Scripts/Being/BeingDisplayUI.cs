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

    private BeingDisplay BeingDisplay;

    public void BeingDisplayUIInit(Being being, BeingDisplay beingDisplay)
    {
        this.BeingDisplay = beingDisplay;

        textHP.SetText(being.Health.HP.ToString());
        textAttack.SetText(being.Attack.ToString());

        Debug.Log($"{being.Speed.Turn} {being.Speed.TurnFrequency} {being.Speed.TurnPercent}");
        textTurn.SetText(being.Speed.TurnsLeft.ToString());
        imageSpeedFill.fillAmount = being.Speed.TurnPercent;
    }
}