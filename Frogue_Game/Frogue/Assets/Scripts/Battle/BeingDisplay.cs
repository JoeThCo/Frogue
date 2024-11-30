using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeingDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [Space(10)]
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private Transform uiTransform;


    public Being Being { get; private set; }
    public bool IsPlayerInteractable { get; private set; }

    public void BeingDisplayInit(Being being, bool isPlayerInteractable)
    {
        Being = being;
        Being.SetBeingDisplay(this);
        IsPlayerInteractable = isPlayerInteractable;

        hpText.SetText(being.HP.ToString());
        damageText.SetText(being.Damage.ToString());

        uiTransform.LookAt(BattleManager.MainCamera.transform.position);
    }

    public void Move(Vector3 newPosition, float moveTime = 0.25f)
    {
        transform.DOMove(newPosition, moveTime).SetEase(Ease.Linear);
    }

    public IEnumerator MoveToandFrom(BeingDisplay beingDisplay, float moveTime = .25f)
    {
        Vector3 startPos = transform.position;

        Move(beingDisplay.transform.position, moveTime);
        yield return new WaitForSeconds(moveTime);

        Move(startPos, moveTime);
        yield return new WaitForSeconds(moveTime);
    }

    public void Rotate(float y)
    {
        rotateTransform.Rotate(Vector3.up * y);
    }

    public void OnDead()
    {
        Destroy(gameObject);
    }
}