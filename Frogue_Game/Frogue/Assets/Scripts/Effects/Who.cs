using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Who", menuName = "ScriptableObjects/Who")]
public class Who : ScriptableObject
{
    [SerializeField]
    private WhoEntry[] whoEntries = new WhoEntry[]
    {
        new WhoEntry { Coord = Vector2Int.up, IsIncluded = true },
        new WhoEntry { Coord = Vector2Int.one, IsIncluded = true },
        new WhoEntry { Coord = Vector2Int.left, IsIncluded = true },
        new WhoEntry { Coord = Vector2Int.zero, IsIncluded = true },
        new WhoEntry { Coord = Vector2Int.right, IsIncluded = true },
        new WhoEntry { Coord = Vector2Int.down, IsIncluded = true },
        new WhoEntry { Coord = Vector2Int.left + Vector2Int.up, IsIncluded = true }
    };

    public Vector2Int[] GetWho()
    {
        List<Vector2Int> output = new List<Vector2Int>();

        foreach (WhoEntry entry in whoEntries)
        {
            if (entry.IsIncluded)
            {
                output.Add(entry.Coord);
            }
        }

        return output.ToArray();
    }

    public override string ToString()
    {
        return name;
    }

    [System.Serializable]
    public struct WhoEntry
    {
        public Vector2Int Coord;
        public bool IsIncluded;
    }
}