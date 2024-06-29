using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BeingBattleBus
{
    public delegate void BattleEvent();

    public static event BattleEvent BattleStart;
    public static event BattleEvent BattleHalf;
    public static event BattleEvent BattleEnd;

    public static void EmitBattleStart()
    {
        BattleStart?.Invoke();
    }

    public static void EmitBattleEnd()
    {
        BattleEnd?.Invoke();
    }

    public static void EmitBattleHalf() 
    {
        BattleHalf?.Invoke();
    }
}