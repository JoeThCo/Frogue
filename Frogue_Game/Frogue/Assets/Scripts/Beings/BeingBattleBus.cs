using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BeingBattleBus
{
    public delegate void BattleEvent();

    public static event BattleEvent FightStart;
    public static event BattleEvent FightHalf;
    public static event BattleEvent FightEnd;

    public static event BattleEvent BattleOver;

    public static event BattleEvent GridRefresh;


    public static void EmitFightStart()
    {
        FightStart?.Invoke();
    }

    public static void EmitFightEnd()
    {
        FightEnd?.Invoke();
    }

    public static void EmitFightHalf()
    {
        FightHalf?.Invoke();
    }

    public static void EmitBattleOver()
    {
        BattleOver?.Invoke();
    }

    public static void EmitGridRefresh()
    {
        GridRefresh?.Invoke();
    }
}