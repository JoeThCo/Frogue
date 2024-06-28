using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeingBattleUI : MonoBehaviour
{
    [SerializeField] private Button fight;

    private void Start()
    {
        BeingBattleBus.BattleStart += BeingBattleBus_BattleStart;
        BeingBattleBus.BattleEnd += BeingBattleBus_BattleEnd;
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