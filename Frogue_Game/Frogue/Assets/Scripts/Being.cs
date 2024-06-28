using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Being : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer outlineRenderer;

    public void BeingInit()
    {
        BeingBattleBus.BattleStart += BeingBattleBus_BattleStart;

        spriteRenderer.color = Random.ColorHSV();
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
        outlineRenderer.enabled = true;
    }

    public void OnDeselect()
    {
        outlineRenderer.enabled = false;
    }

    public IEnumerator TweenToBeing(Being otherBeing, float totalTime = 1)
    {
        spriteRenderer.sortingOrder++;
        outlineRenderer.sortingOrder++;

        OnSelect();

        Vector3 startPos = gameObject.transform.position;
        float halfTime = totalTime * 0.5f;

        gameObject.transform.DOMove(otherBeing.transform.position, halfTime);
        yield return new WaitForSeconds(halfTime);

        gameObject.transform.DOMove(startPos, halfTime);
        yield return new WaitForSeconds(halfTime);

        OnDeselect();

        spriteRenderer.sortingOrder--;
        outlineRenderer.sortingOrder--;
    }
}