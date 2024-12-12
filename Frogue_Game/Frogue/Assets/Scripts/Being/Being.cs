using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being
{
    public int ID { get; private set; }
    public Health Health { get; private set; }
    public int Attack { get; private set; }
    public int Speed { get; private set; }
    public Vector2Int Coords { get; set; }
    public BeingDisplay BeingDisplay { get; private set; }

    public Being(Vector2Int coords)
    {
        BeingInit init = ResourceLoader.GetBeingInit();

        Health = new Health(init);

        Attack = init.GetDamage();
        Speed = init.GetSpeed();

        Coords = coords;
        ID = Coords.y + (Coords.x * Board.BOARD_SIZE) + 1;
    }

    public Being(Being being)
    {
        Health = new Health(being.Health.HP);

        Attack = being.Attack;
        Speed = being.Attack;

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
        return $"{ID} | ({Coords})| [{Health.HP}/{Health.MaxHP} | {Attack}]";
    }
}