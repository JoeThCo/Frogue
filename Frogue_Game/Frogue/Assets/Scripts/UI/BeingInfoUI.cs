using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BeingInfoUI : MonoBehaviour
{
    [SerializeField] private Transform infoParent;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private Image typeImage;
    [Space(10)]
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        PlayerGridModify.BeingSlotSelected += PlayerGridModify_BeingSlotSelected;
        PlayerGridModify.BeingSlotCleared += PlayerGridModify_BeingSlotCleared;
        BeingBattle.FightStart += BeingBattle_FightStart;

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        PlayerGridModify_BeingSlotCleared(null);
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        PlayerGridModify.BeingSlotSelected -= PlayerGridModify_BeingSlotSelected;
        PlayerGridModify.BeingSlotCleared -= PlayerGridModify_BeingSlotCleared;
        BeingBattle.FightStart -= BeingBattle_FightStart;
        
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void PlayerGridModify_BeingSlotSelected(BeingSlot beingSlot)
    {
        infoParent.gameObject.SetActive(true);

        nameText.SetText(beingSlot.Being.BeingInfo.name);
        damageText.SetText($"Damage:{beingSlot.Being.Damage}");
        typeImage.sprite = beingSlot.Being.Types.GetIcon();

        healthBar.HealthBarInit(beingSlot.Being);
    }

    private void PlayerGridModify_BeingSlotCleared(BeingSlot obj)
    {
        infoParent.gameObject.SetActive(false);
    }

    private void BeingBattle_FightStart()
    {
        infoParent.gameObject.SetActive(false);
    }
}