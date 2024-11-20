using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being
{
    public int ID { get; private set; }
    public int HP { get; private set; }
    public int Damage { get; private set; }
    public Vector2Int Coords { get; private set; }

    public bool IsDead { get { return HP <= 0; } }

    public BeingDisplay BeingDisplay { get; private set; }
    public Board Board { get; private set; }

    public Being()
    {
        this.ID = Random.Range(-10000, 10000);

        BeingInfo info = ResourceLoader.GetBeingInfo();
        HP = info.GetHealth();
        Damage = info.GetDamage();
    }

    public Being DeepCopy()
    {
        Being newBeing = new Being();

        newBeing.SetCoords(this.Coords);
        newBeing.SetBeingDisplay(this.BeingDisplay);
        newBeing.HP = this.HP;
        newBeing.Damage = this.Damage;
        newBeing.ID = this.ID;

        return newBeing;
    }

    public void SetBeingDisplay(BeingDisplay beingDisplay)
    {
        this.BeingDisplay = beingDisplay;
    }

    public void SetCoords(Vector2Int coords)
    {
        this.Coords = coords;
    }

    public void TakeDamage(Being b)
    {
        this.HP -= b.Damage;
    }

    public override string ToString()
    {
        return $"{ID} ({Coords})| HP: {HP}, ATK: {Damage}";
    }
}