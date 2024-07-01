using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int DamageAmount { get; private set; }

    public void DamageInit()
    {
        DamageAmount = Random.Range(2, 7);
    }
}