using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilityMenu : Menu
{
    [SerializeField] private AbilityInfoUI abilityInfoUIPrefab;

    public override void MenuInit()
    {
        PlayerGridModify.BeingSlotSelected += PlayerGridModify_BeingSlotSelected;
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        PlayerGridModify.BeingSlotSelected -= PlayerGridModify_BeingSlotSelected;
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void PlayerGridModify_BeingSlotSelected(BeingSlot obj)
    {
        foreach (Transform child in menuParent.transform)
        {
            Destroy(child.gameObject);
        }

        if (obj.Being.BeingInfo == null) return;

        foreach (Ability ability in obj.Being.BeingInfo.GetAbilities())
        {
            AbilityInfoUI abilityInfoUI = Instantiate(abilityInfoUIPrefab, menuParent);
            abilityInfoUI.AbilityInfoInit(ability);
        }
    }
}