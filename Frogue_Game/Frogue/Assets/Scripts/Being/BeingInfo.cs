using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingInfo
{
    public int ID { get; private set; }
    public int HP { get; private set; }
    public int Attack { get; private set; }
    public Vector2Int Coords { get; private set; }
    public BeingDisplay BeingDisplay { get; private set; }
    public bool IsDead { get { return HP <= 0; } }

    public BeingInfo(BeingInit beingInit, Vector2Int coords)
    {
        BeingInit info = ResourceLoader.GetBeingInit();
        HP = info.GetHealth();
        Attack = info.GetDamage();

        Coords = coords;
        ID = Coords.y + (Coords.x * Board.BOARD_SIZE) + 1;
    }

    public BeingInfo(int iD, int hP, int damage, Vector2Int coords, BeingDisplay beingDisplay)
    {
        ID = iD;
        HP = hP;
        Attack = damage;
        Coords = coords;
        BeingDisplay = beingDisplay;
    }

    public void UpdateCoords(Vector2Int coords)
    {
        this.Coords = coords;
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
    }

    public void SetBeingDiplay(BeingDisplay beingDisplay)
    {
        this.BeingDisplay = beingDisplay;
    }
}
