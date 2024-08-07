using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Being : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Damage Damage { get; private set; }
    public Health Health { get; private set; }
    public Effects Effects { get; private set; }
    public Types Types { get; private set; }
    public BeingSO BeingInfo { get; private set; }

    public void BeingInit(BeingSO beingSO)
    {
        BeingInfo = beingSO;

        Effects = new Effects(beingSO.GetEffects());
        Types = new Types(beingSO.GetTypes());

        Health = new Health(beingSO.GetHealth());
        Health.OnDeath += Health_OnDeath;

        Damage = new Damage(Effects, beingSO.GetDamage());

        spriteRenderer.color = Types.GetMainColor();
    }

    private void Health_OnDeath()
    {
        Health.OnDeath -= Health_OnDeath;
        Destroy(gameObject);
    }

    public IEnumerator DamageTween(Being otherBeing, float totalTime = .5f)
    {
        float halfTime = totalTime * .5f;
        Vector3 startPosition = transform.position;

        transform.DOMove(otherBeing.transform.position, halfTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(halfTime);

        otherBeing.Health.TakeDamage(this);

        transform.DOMove(startPosition, halfTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(halfTime);
    }

    public void ChangeParentSlot(BeingSlot slot, float swapTime = .25f)
    {
        slot.Being = this;

        transform.SetParent(slot.transform);
        transform.DOLocalMove(Vector2.zero, swapTime).SetEase(Ease.Linear);
    }
}