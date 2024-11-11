using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    public Board Player { get; private set; }
    public Board Baddie { get; private set; }

    public Battle()
    {
        Player = new Board();
        Baddie = new Board();
    }
}