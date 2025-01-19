using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeingDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI IDText;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;

    public void BeingDisplayUIInit(Being being)
    {
        transform.LookAt(-GameManager.MainCamera.transform.position);

        IDText.SetText(being.ID.ToString());
        healthText.SetText(being.Health.ToString());
        attackText.SetText(being.Attack.ToString());
    }
}