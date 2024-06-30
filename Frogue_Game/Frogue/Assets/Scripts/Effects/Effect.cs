using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Effect")]
public class Effect : ScriptableObject
{
    [SerializeField] private ParticleSystem vfx;
    public Who Who { get; private set; }

    public virtual void EffectInit()
    {
        Who = ResourceManager.GetWho();
    }

    public virtual IEnumerator ApplyEffect(BattleState battleState)
    {
        if (vfx != null)
        {
            yield return battleState.OffenseBeingGrid.ApplyVFX(battleState.AttackingBeing, Who, vfx);
        }
    }

    public override string ToString()
    {
        return name;
    }
}