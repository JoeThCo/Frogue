using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void BeingInit()
    {
        spriteRenderer.color = Random.ColorHSV();
    }

    public override string ToString()
    {
        return string.Empty;
    }
}