using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BeingSlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer outline;
    public Being Being { get; set; }
    public bool isPlayerInteractable { get; private set; }
    public Vector2Int Coords { get; set; }
    private Vector3 defaultScale = Vector3.zero;
    [Space(10)]
    [SerializeField] private Color defaultOutline;
    [SerializeField] private Color selectedOutline;

    public void BeingSlotInit(Vector2Int coords, bool isPlayerInteractable)
    {
        this.isPlayerInteractable = isPlayerInteractable;
        this.Coords = coords;
        this.defaultScale = outline.transform.localScale;

        OnDeselect();

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        BeingBattle.FightStart += BeingBattle_FightStart;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        BeingBattle.FightStart -= BeingBattle_FightStart;
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void BeingBattle_FightStart()
    {
        OnDeselect();
    }

    public override string ToString()
    {
        return Being.ToString() + " " + Coords.ToString();
    }

    public void OnSelect()
    {
        outline.color = selectedOutline;
        outline.transform.localScale = defaultScale * 1.25f;
    }

    public void OnDeselect()
    {
        outline.color = defaultOutline;
        outline.transform.localScale = defaultScale;
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        BeingSlot compare = other as BeingSlot;

        return compare.Coords.Equals(Coords) &&
            Being.Equals(compare.Being) &&
            compare.isPlayerInteractable == isPlayerInteractable;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public void SwapBeings(BeingSlot other)
    {
        if (other.Being == null)
        {
            Being.transform.SetParent(other.transform);

            other.Being = Being;
            Being = null;

            other.Being.transform.DOLocalMove(Vector2.zero, .25f);
        }
        else
        {
            BeingSlot tempSlot = this;
            Being.transform.SetParent(other.transform);
            other.Being.transform.SetParent(tempSlot.transform);

            Being tempController = Being;
            Being = other.Being;
            other.Being = tempController;

            Being.transform.DOLocalMove(Vector2.zero, .25f);
            other.Being.transform.DOLocalMove(Vector2.zero, .25f);
        }
    }
}