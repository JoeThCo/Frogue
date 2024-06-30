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

        yield return GridFight(playerGrid, baddieGrid);

        BeingBattleBus.EmitBattleHalf();
        yield return new WaitForSeconds(1);

        yield return GridFight(baddieGrid, playerGrid);

        BeingBattleBus.EmitBattleEnd();
    }

    IEnumerator GridFight(BeingGrid offense, BeingGrid defense)
    {
        foreach (Being current in offense.GetAliveBeings())
        {
            foreach (Ability ability in current.Abilities)
            {
                BattleState battleState = new BattleState(current, defense.GetFirstBeing(), offense, defense);
                yield return ability.TryApplyEffect(battleState);
            }

            yield return current.TweenToBeing(defense.GetFirstBeing());
        }
    }
}