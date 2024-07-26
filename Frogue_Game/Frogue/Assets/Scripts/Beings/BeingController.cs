using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Being Being { get; private set; }

    public void BeingControllerInit(Being being)
    {
        Being = being;
        Debug.Log(Being.name);
        spriteRenderer.color = Being.Types.GetMainColor();
    }
}