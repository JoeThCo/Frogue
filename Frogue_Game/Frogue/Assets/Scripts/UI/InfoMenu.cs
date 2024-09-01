using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoMenu : Menu
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private Image typeImage;
    [Space(10)]
    [SerializeField] private HealthBar healthBar;

    public override void MenuInit()
    {
        PlayerGridModify.BeingSlotSelected += BeingGridModify_BeingSlotSelected;
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged; ;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        PlayerGridModify.BeingSlotSelected -= BeingGridModify_BeingSlotSelected;
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void BeingGridModify_BeingSlotSelected(BeingSlot beingSlot)
    {
        nameText.SetText(beingSlot.Being.BeingInfo.name);
        damageText.SetText($"Damage: {beingSlot.Being.Damage}");
        typeImage.sprite = beingSlot.Being.Types.GetIcon();

        healthBar.HealthBarInit(beingSlot.Being);
    }
}