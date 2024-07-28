using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI companyText;


    private void Start()
    {
        versionText.SetText(Application.version);
        titleText.SetText(Application.productName);
        companyText.SetText(Application.companyName);
    }

    public void PlayGame() 
    {
        SceneManager.LoadScene("Demo");
    }
}
