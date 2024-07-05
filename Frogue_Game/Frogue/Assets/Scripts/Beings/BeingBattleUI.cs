using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeingBattleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI seed;
    [SerializeField] private Button fight;

    private void Start()
    {
        BeingBattleBus.FightStart += BeingBattleBus_BattleStart;
        BeingBattleBus.FightEnd += BeingBattleBus_BattleEnd;

        seed.SetText("Seed: " + GameManager.Seed);
    }

    private void BeingBattleBus_BattleStart()
    {
        fight.gameObject.SetActive(false);
    }

    private void BeingBattleBus_BattleEnd()
    {
        fight.gameObject.SetActive(true);
    }
}