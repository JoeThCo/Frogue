using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Baddie", menuName = "ScriptableObject/Baddie")]
public class BaddieSO : BeingSO
{
    public BulkAttack[] BulkAttacks;

    public BulkAttack GetRandomBulkAttack()
    {
        return BulkAttacks[GameManager.Random.Next(0, BulkAttacks.Length)];
    }
}