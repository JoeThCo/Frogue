using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingInfo
{
    public int ID { get; private set; }
    public int HP { get; private set; }
    public int Damage { get; private set; }
    public Vector2Int Coords { get; private set; }
    public bool IsDead { get { return HP <= 0; } }

    public BeingInfo(BeingInit beingInit, Vector2Int coords)
    {
        this.ID = Random.Range(-10000, 10000);

        BeingInit info = ResourceLoader.GetBeingInit();
        HP = info.GetHealth();
        Damage = info.GetDamage();

        Coords = coords;
    }

    public BeingInfo(int iD, int hP, int damage, Vector2Int coords)
    {
        ID = iD;
        HP = hP;
        Damage = damage;
        Coords = coords;
    }

    public void UpdateCoords(Vector2Int coords)
    {
        this.Coords = coords;
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
    }
}
