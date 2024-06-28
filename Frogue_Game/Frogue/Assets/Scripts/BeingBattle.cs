using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingBattle : MonoBehaviour
{
    [SerializeField] BeingGrid playerGrid;
    [SerializeField] BeingGrid baddieGrid;


    public void Fight()
    {
        Debug.Log(playerGrid.GetAliveBeings().Length);
        Debug.Log(baddieGrid.GetAliveBeings().Length);
    }
}
