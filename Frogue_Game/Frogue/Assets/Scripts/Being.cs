using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer outlineRenderer;

    public void BeingInit()
    {
        spriteRenderer.color = Random.ColorHSV();
        OnDeselect();
    }

    public override string ToString()
    {
        return string.Empty;
    }

    public void OnSelect()
    {
        outlineRenderer.enabled = true;
    }

    public void OnDeselect()
    {
        outlineRenderer.enabled = false;
    }
}