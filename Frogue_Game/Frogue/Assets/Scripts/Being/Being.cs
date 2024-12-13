using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being
{
    public int ID { get; private set; }
    public Health Health { get; private set; }
    public int Attack { get; private set; }
    public Speed Speed { get; private set; }
    public Vector2Int Coords { get; set; }
    public BeingDisplay BeingDisplay { get; private set; }

    public Being(Vector2Int coords)
    {
        BeingInit init = ResourceLoader.GetBeingInit();

        Health = new Health(init.GetHealth());
        Attack = init.GetDamage();
        Speed = new Speed(init.GetTurnFrequency());

        Coords = coords;
        ID = Coords.y + (Coords.x * BoardDisplay.BOARD_SIZE) + 1;
    }

    public Being(Being being)
    {
        Health = new Health(being.Health.HP);

        Attack = being.Attack;
        Speed = new Speed(being.Speed.Turn, being.Speed.TurnFrequency);

        Coords = being.Coords;

        BeingDisplay = being.BeingDisplay;
        ID = being.ID;
    }

    public void SetBeingDiplay(BeingDisplay beingDisplay)
    {
        this.BeingDisplay = beingDisplay;
    }

    public override string ToString()
    {
        return $"{ID} | ({Coords})| [{Health.HP}/{Health.MaxHP} | {Attack} | {Speed.Turn} / {Speed.TurnFrequency}]";
    }
}