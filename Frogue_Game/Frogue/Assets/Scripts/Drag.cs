using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 dragPosition;

    public void Update()
    {
        if (dragging) 
        {
            dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPosition.z = 0;
            transform.position = dragPosition;
        }
    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
}