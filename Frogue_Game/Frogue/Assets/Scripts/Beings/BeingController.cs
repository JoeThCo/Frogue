using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingController : MonoBehaviour
{
    public Being Being { get; set; }
    [SerializeField] private Transform BeingModel;

    public void BeingControllerInit(Being being, bool isPlayerInteractable)
    {
        this.Being = being;
        Being.Health.OnDeath += Health_OnDeath;

        if (!isPlayerInteractable)
            BeingModel.transform.Rotate(Vector3.up, 180);
    }

    private void Health_OnDeath()
    {
        Being.Health.OnDeath -= Health_OnDeath;
        Destroy(gameObject);
    }

    public void AddEffect(Effect effect)
    {
        SoundEffectsManager.PlaySFX(effect, this);
    }

    public void TakeDamage(BeingController other, int finalDamage)
    {
        SpawnDamageText(other, finalDamage);
        SoundEffectsManager.PlaySFX("Damage", other);
    }

    void SpawnDamageText(BeingController beingController, int finalDamage)
    {
        DamageTextPopup damageText = GameObject.Instantiate(ResourceManager.GetUI("DamageTextPopUp")).GetComponent<DamageTextPopup>();
        damageText.DamageTextPopUpInit(beingController, finalDamage);
    }

    public IEnumerator DamageTween(BeingController otherBeing, float totalTime = .33f)
    {
        float halfTime = totalTime * .5f;
        Vector3 startPosition = transform.position;

        transform.DOMove(otherBeing.transform.position, halfTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(halfTime);

        transform.DOMove(startPosition, halfTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(halfTime);
    }

    public void ChangeParentSlot(BeingSlot slot, float swapTime = .25f)
    {
        slot.BeingController = this;

        transform.SetParent(slot.transform);
        transform.DOLocalMove(Vector2.zero, swapTime).SetEase(Ease.Linear);
    }
}