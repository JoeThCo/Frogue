using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        Health.OnDeath += Health_OnDeath;

        Damage = new Damage(Effects, beingSO.startDamage);
        Speed = new Speed(Effects, beingSO.startSpeed);

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

        otherBeing.Health.TakeDamage(otherBeing);

        transform.DOMove(startPosition, halfTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(halfTime);
    }
}