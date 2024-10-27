using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BeingSlot : MonoBehaviour
{
    public Being Being { get; set; }
    public bool PlayerInteractable { get; private set; }
    public Vector2Int Coords { get; set; }
    public Vector3 WorldCoords { get; set; }
    private Vector3 defaultScale = Vector3.zero;
    [Space(10)]
    [SerializeField] private Color defaultOutline;
    [SerializeField] private Color selectedOutline;

    public void BeingSlotInit(Vector2Int coords, bool playerInteractable)
    {
        this.PlayerInteractable = playerInteractable;

        this.Coords = coords;
        this.WorldCoords = new Vector3(coords.x, 0, coords.y);

        if (!PlayerInteractable)
            transform.Rotate(Vector3.up, 180);

        OnDeselect();

        BeingBattle.FightStart += BeingBattle_FightStart;
        PlayerGridModify.BeingSlotCleared += PlayerGridModify_BeingSlotCleared;

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        BeingBattle.FightStart -= BeingBattle_FightStart;
        PlayerGridModify.BeingSlotCleared -= PlayerGridModify_BeingSlotCleared;

        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void PlayerGridModify_BeingSlotCleared(BeingSlot obj)
    {
        OnDeselect();
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
    }

    public void OnDeselect()
    {
    }

    public override bool Equals(object other)
    {
        if (other == null) return false;
        BeingSlot compare = other as BeingSlot;

        return compare.Coords.Equals(Coords) &&
            Being.Equals(compare.Being) &&
            compare.PlayerInteractable == PlayerInteractable;
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