using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Being", menuName = "ScriptableObject/Being")]
public class Being : ScriptableObject
{
    [SerializeField][Range(1, 10)] private int startDamage;
    [SerializeField][Range(1, 10)] private int startHealth;
    [SerializeField][Range(1, 10)] private int startSpeed;
    [SerializeField] private Effect[] startEffects;
    [SerializeField] private BeingType[] startTypes;

    public Damage Damage { get; private set; }
    public Health Health { get; private set; }
    public Speed Speed { get; private set; }
    public Effects Effects { get; private set; }
    public Types Types { get; private set; }

    public void BeingInit()
    {
        Effects = new Effects(startEffects);

        Types = new Types(startTypes);
        Health = new Health(startHealth);

        Damage = new Damage(Effects, startDamage);
        Speed = new Speed(Effects, startSpeed);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}