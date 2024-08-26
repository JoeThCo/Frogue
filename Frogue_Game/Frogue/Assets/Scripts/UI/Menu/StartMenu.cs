using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : Menu
{
    [SerializeField] private TextMeshProUGUI versionText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI companyText;

    public override void MenuInit()
    {
        versionText.SetText(Application.version);
        titleText.SetText(Application.productName);
        companyText.SetText(Application.companyName);
    }
}
