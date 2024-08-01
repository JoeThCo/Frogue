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
        BeingBattle.FightStart += BeingBattleBus_BattleStart;
        BeingBattle.FightEnd += BeingBattleBus_BattleEnd;

        seed.SetText("Seed: " + GameManager.Seed);
    }

    public void OnDisable()
    {
        BeingBattle.FightStart -= BeingBattleBus_BattleStart;
        BeingBattle.FightEnd -= BeingBattleBus_BattleEnd;
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