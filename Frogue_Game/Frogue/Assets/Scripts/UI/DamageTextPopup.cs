using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private float destroyTime = .35f;

    public void DamageTextPopUpInit(Being being, int finalDamage)
    {
        damageText.SetText($"-{finalDamage}");
        transform.position = being.transform.position;
        Destroy(gameObject, destroyTime);
    }
}