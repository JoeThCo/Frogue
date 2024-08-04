using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Baddie", menuName = "ScriptableObject/Baddie")]
public class BaddieSO : BeingSO
{
    public Who[] WhoToAttack;

    public Who GetRandomBulkAttack()
    {
        return WhoToAttack[GameManager.Random.Next(0, WhoToAttack.Length)];
    }
}