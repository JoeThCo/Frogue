using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Being being;
    [Space(10)]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        if (being != null)
            HealthBarInit(being);
    }

    public void HealthBarInit(Being being)
    {
        being.Health.OnHealthChanged += Health_OnHealthChanged;
        Health_OnHealthChanged(being.Health);
    }

    private void OnDisable()
    {
        if (being != null)
            being.Health.OnHealthChanged -= Health_OnHealthChanged;
    }

    private void Health_OnHealthChanged(Health health)
    {
        healthBar.fillAmount = health.GetPercent();
        healthText.SetText(health.ToString());
    }
}