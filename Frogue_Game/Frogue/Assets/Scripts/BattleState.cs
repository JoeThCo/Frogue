using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState
{
    public Being AttackingBeing { get; private set; }
    public Being DefendingBeing { get; private set; }

    public PlayerGrid OffenseBeingGrid { get; private set; }
    public PlayerGrid DefenseBeingGrid { get; private set; }

    public BattleState(Being AttackingBeing, Being DefendingBeing, PlayerGrid OffenseBeingGrid, PlayerGrid DefenseBeingGrid)
    {
        this.AttackingBeing = AttackingBeing;
        this.DefendingBeing = DefendingBeing;

        this.OffenseBeingGrid = OffenseBeingGrid;
        this.DefenseBeingGrid = DefenseBeingGrid;
    }
}