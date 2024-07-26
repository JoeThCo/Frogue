using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Being : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Damage Damage { get; private set; }
    public Health Health { get; private set; }
    public Speed Speed { get; private set; }
    public Effects Effects { get; private set; }
    public Types Types { get; private set; }

    public void BeingInit(BeingSO beingSO)
    {
        Effects = new Effects(beingSO.startEffects);

        Types = new Types(beingSO.startTypes);
        Health = new Health(beingSO.startHealth);

        Damage = new Damage(Effects, beingSO.startDamage);
        Speed = new Speed(Effects, beingSO.startSpeed);

        spriteRenderer.color = Types.GetMainColor();
    }
}