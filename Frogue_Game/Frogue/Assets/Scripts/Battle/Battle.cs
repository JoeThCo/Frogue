using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Battle
{
    public int PlusMinus { get; private set; } = 0;
    public BattleAction[] BattleActions { get; private set; }

    public Being[] PlayerOutput { get; private set; }
    public Being[] BaddieOutput { get; private set; }

    public Battle(Board playerBoard, Board baddieBoard)
    {

    }
}