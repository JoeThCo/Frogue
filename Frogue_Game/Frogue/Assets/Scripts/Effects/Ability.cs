using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public class Ability
{
    [SerializeField] private Trigger trigger;
    [SerializeField] private Effect effect;
    [SerializeField] private Who who;

    public Ability() 
    {
        
    }
}