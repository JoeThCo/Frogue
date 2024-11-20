using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    private Board Player { get; set; }
    private Board Baddie { get; set; }

    public Battle(Board _p, Board _b)
    {
        Player = _p.DeepCopy();
        Baddie = _b.DeepCopy();
    }
}