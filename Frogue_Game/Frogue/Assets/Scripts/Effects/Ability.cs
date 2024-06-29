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
        trigger = ResourceManager.GetTrigger();
        effect = ResourceManager.GetEffect();
        who = ResourceManager.GetWho();

        Debug.LogFormat("{0} {1} {2}", trigger.ToString(), effect.ToString(), who.ToString());
    }
}