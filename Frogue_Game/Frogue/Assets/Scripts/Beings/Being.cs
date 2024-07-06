using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Being : MonoBehaviour
{
    public Damage Damage { get; private set; }
    public Health Health { get; private set; }
    public Speed Speed { get; private set; }
    public Effects Effects { get; private set; }
    public Types Types { get; private set; }

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer outline;

    private Vector3 defaultScale = Vector3.zero;

    public void BeingInit()
    {
        Effects = new Effects();

        Types = new Types();
        Health = new Health();

        Damage = new Damage(Effects);
        Speed = new Speed(Effects);

        BeingBattleBus.FightStart += BeingBattleBus_BattleStart;

        this.sprite.color = Types.GetMainColor();
        this.defaultScale = outline.transform.localScale;

        OnDeselect();
    }

    public void OnDestroy()
    {
        BeingBattleBus.FightStart -= BeingBattleBus_BattleStart;
    }

    private void BeingBattleBus_BattleStart()
    {
        OnDeselect();
    }

    public void OnSelect()
    {
        outline.color = Color.white;
        outline.transform.localScale = defaultScale * 1.25f;
    }

    public void OnDeselect()
    {
        outline.color = Color.black;
        outline.transform.localScale = defaultScale;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    public IEnumerator DamageTween(Being otherBeing, float totalTime = .75f)
    {
        sprite.sortingOrder++;
        outline.sortingOrder++;

        OnSelect();

        Vector3 startPos = gameObject.transform.position;
        float halfTime = totalTime * 0.5f;

        gameObject.transform.DOMove(otherBeing.transform.position, halfTime);
        yield return new WaitForSeconds(halfTime);

        otherBeing.Health.TakeDamage(Damage);
        SpawnDamageTextPopUp(otherBeing, Damage);

        gameObject.transform.DOMove(startPos, halfTime);
        yield return new WaitForSeconds(halfTime);

        OnDeselect();

        sprite.sortingOrder--;
        outline.sortingOrder--;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    private void SpawnDamageTextPopUp(Being otherBeing, Damage damage, float destroyTime = .35f)
    {
        DamageTextPopup damageTextPopup = Instantiate(ResourceManager.GetUI("DamageTextPopUp").GetComponent<DamageTextPopup>(), otherBeing.transform.position, Quaternion.identity);
        damageTextPopup.DamageTextPopUpInit(damage);
    }
}