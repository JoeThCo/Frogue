using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeingDisplay : MonoBehaviour
{
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private Transform uiTransform;
    [Space(10)]
    [SerializeField] private BeingDisplayUI beingDisplayUI;
    [Space(10)]
    [SerializeField] private float LerpTime = .25f;
    [SerializeField] private Ease Ease = Ease.Linear;

    public Being Being { get; private set; }
    public Vector3 ReturnPosition { get; set; }
    public bool IsPlayerInteractable { get; set; }

    public void BeingDisplayInit(Being being, bool isPlayerInteractable)
    {
        being.BeingDisplay = this;
        this.Being = being;
        gameObject.name = Being.ToString();

        beingDisplayUI.BeingDisplayUIInit(being);
        IsPlayerInteractable = isPlayerInteractable;

        uiTransform.LookAt(BattleManager.MainCamera.transform.position);

        PlayerModify.SelectBeingDisplay += PlayerModify_SelectBeingDisplay;
        PlayerModify.UnSelectBeingDisplay += PlayerModify_UnSelectBeingDisplay;
        PlayerModify.ClearBeingDisplay += PlayerModify_ClearBeingDisplay;
    }

    private void OnDisable()
    {
        PlayerModify.SelectBeingDisplay -= PlayerModify_SelectBeingDisplay;
        PlayerModify.UnSelectBeingDisplay -= PlayerModify_UnSelectBeingDisplay;
        PlayerModify.ClearBeingDisplay -= PlayerModify_ClearBeingDisplay; ;
    }

    public void SetBeing(Being being) 
    {
        this.Being = being;
    }

    #region Events
    private void PlayerModify_SelectBeingDisplay(BeingDisplay obj)
    {
        if (obj == null || !obj.Equals(this))
        {
            ScaleDown();
            return;
        }

        ScaleUp();
    }

    private void PlayerModify_UnSelectBeingDisplay(BeingDisplay obj)
    {
        if (obj == null || !obj.Equals(this)) return;
        ScaleDown();
    }

    private void PlayerModify_ClearBeingDisplay()
    {
        ScaleDown();
    }
    #endregion

    #region Move
    public void Move(Vector3 newPosition)
    {
        transform.DOMove(newPosition, LerpTime).SetEase(Ease);
    }

    public IEnumerator MoveTo(BeingDisplay beingDisplay)
    {
        Move(beingDisplay.transform.position);
        yield return new WaitForSeconds(LerpTime);
    }

    public IEnumerator MoveToReturn()
    {
        Move(ReturnPosition);
        yield return new WaitForSeconds(LerpTime);
    }

    public void SaveReturnPostion()
    {
        ReturnPosition = transform.position;
    }
    #endregion

    public void Rotate(float y)
    {
        rotateTransform.Rotate(Vector3.up * y);
    }

    private void ScaleUp()
    {
        transform.DOScale(1.5f, LerpTime);
    }

    private void ScaleDown()
    {
        transform.DOScale(1.0f, LerpTime);
    }

    public void OnDead()
    {
        Destroy(gameObject);
    }

    public void OnDamage(DamageAction damageAction) 
    {
        transform.DOShakeScale(LerpTime * 0.5f, 3);
        beingDisplayUI.OnDamage(damageAction);
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        BeingDisplay otherDisplay = other as BeingDisplay;

        return otherDisplay.Being.Coords.Equals(Being.Coords) &&
            otherDisplay.IsPlayerInteractable.Equals(IsPlayerInteractable);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        if (Being != null)
            return $"{Being.ToString()}";
        return $"EMPTY";
    }
}