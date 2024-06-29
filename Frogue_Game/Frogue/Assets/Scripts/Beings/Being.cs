using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Being : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer outline;

    private Vector3 defaultScale = Vector3.zero;

    List<Ability> Abilities = new List<Ability>();

    public void BeingInit()
    {
        BeingBattleBus.BattleStart += BeingBattleBus_BattleStart;
        Abilities.Add(new Ability());

        this.sprite.color = Random.ColorHSV();
        this.defaultScale = outline.transform.localScale;

        OnDeselect();
    }

    private void BeingBattleBus_BattleStart()
    {
        OnDeselect();
    }

    public override string ToString()
    {
        return string.Empty;
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

    public IEnumerator TweenToBeing(Being otherBeing, float totalTime = .75f)
    {
        sprite.sortingOrder++;
        outline.sortingOrder++;

        OnSelect();

        Vector3 startPos = gameObject.transform.position;
        float halfTime = totalTime * 0.5f;

        gameObject.transform.DOMove(otherBeing.transform.position, halfTime);
        yield return new WaitForSeconds(halfTime);

        gameObject.transform.DOMove(startPos, halfTime);
        yield return new WaitForSeconds(halfTime);

        OnDeselect();

        sprite.sortingOrder--;
        outline.sortingOrder--;
    }
}