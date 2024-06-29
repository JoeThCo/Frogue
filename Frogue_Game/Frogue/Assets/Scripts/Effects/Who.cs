using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Who", menuName = "ScriptableObjects/Who")]
public class Who : ScriptableObject
{
    public WhoEntry[] whoEntries = new WhoEntry[]
    {
        new WhoEntry { Coord = Vector2Int.up, isIncluded = true },
        new WhoEntry { Coord = Vector2Int.one, isIncluded = true },
        new WhoEntry { Coord = Vector2Int.left, isIncluded = true },
        new WhoEntry { Coord = Vector2Int.right, isIncluded = true },
        new WhoEntry { Coord = Vector2Int.down, isIncluded = true },
        new WhoEntry { Coord = Vector2Int.left + Vector2Int.up, isIncluded = true }
    };

    public override string ToString()
    {
        return name;
    }

    [System.Serializable]
    public struct WhoEntry
    {
        public Vector2Int Coord;
        public bool isIncluded;
    }
}