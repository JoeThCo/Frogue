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
    [Space(10)]
    [SerializeField] private float MoveTime = .25f;
    [SerializeField] private Ease Ease = Ease.Linear;

    public float HalfMoveTime { get { return MoveTime * 0.5f; } }
    public Vector3 ReturnPosition { get; set; }
    public Vector2Int Coords { get; private set; }
    public bool IsPlayerInteractable { get; private set; }

    public void BeingDisplayInit(Being being, bool isPlayerInteractable)
    {
        this.Coords = being.BeingInfo.Coords;
        being.SetBeingDisplay(this);
        IsPlayerInteractable = isPlayerInteractable;

        hpText.SetText(being.BeingInfo.HP.ToString());
        damageText.SetText(being.BeingInfo.Damage.ToString());

        uiTransform.LookAt(BattleManager.MainCamera.transform.position);
    }

    public void UpdateCoords(Vector2Int coords) 
    {
        this.Coords = coords;
    }

    #region Move
    public void Move(Vector3 newPosition)
    {
        transform.DOMove(newPosition, MoveTime).SetEase(Ease);
    }

    public IEnumerator MoveTo(BeingDisplay beingDisplay)
    {
        Move(beingDisplay.transform.position);
        yield return new WaitForSeconds(MoveTime);
    }

    public IEnumerator MoveToReturn()
    {
        Move(ReturnPosition);
        yield return new WaitForSeconds(MoveTime);
    }

    public void SaveReturnPostion()
    {
        ReturnPosition = transform.position;
    }
    #endregion

    public IEnumerator OnDamage()
    {
        ResourceLoader.SpawnParticle("Damage", transform.position);
        ResourceLoader.SpawnSoundEffect("Damage", transform.position);

        yield return transform.DOShakePosition(MoveTime, strength: .5f);
        transform.DOShakeRotation(MoveTime);
        transform.DOShakeScale(MoveTime);
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