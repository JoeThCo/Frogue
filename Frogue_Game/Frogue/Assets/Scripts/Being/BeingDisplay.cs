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

    public Vector3 ReturnPosition { get; set; }
    public Vector2Int Coords { get; set; }
    public bool IsPlayerInteractable { get; set; }


    public void BeingDisplayInit(Being being, bool isPlayerInteractable)
    {
        this.Coords = being.Coords;
        being.SetBeingDiplay(this);
        beingDisplayUI.BeingDisplayUIInit(being, this);
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

    public override bool Equals(object other)
    {
        if (other == null) return false;
        BeingDisplay otherDisplay = other as BeingDisplay;

        return otherDisplay.Coords.Equals(Coords) &&
            otherDisplay.IsPlayerInteractable.Equals(IsPlayerInteractable);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}