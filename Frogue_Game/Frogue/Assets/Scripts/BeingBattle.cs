using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingBattle : MonoBehaviour
{
    [SerializeField] BeingGrid playerGrid;
    [SerializeField] BeingGrid baddieGrid;

    public void Fight()
    {
        StartCoroutine(FightI());
    }

    public IEnumerator FightI()
    {
        BeingBattleBus.EmitBattleStart();

        foreach (Being being in playerGrid.GetAliveBeings())
        {
            yield return being.TweenToBeing(baddieGrid.GetFirstBeing());
        }

        yield return new WaitForSeconds(1);

        foreach (Being being in baddieGrid.GetAliveBeings())
        {
            yield return being.TweenToBeing(playerGrid.GetFirstBeing());
        }

        BeingBattleBus.EmitBattleEnd();
    }
}