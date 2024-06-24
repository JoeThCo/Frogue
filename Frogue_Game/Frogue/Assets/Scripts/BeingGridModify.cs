using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeingGridModify : MonoBehaviour
{
    [SerializeField] private BeingGrid beingGrid;
    [SerializeField] private BeingSlot selectedSlot;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            BeingSlot otherBeingSlot = hit.collider.gameObject.GetComponent<BeingSlot>();
            if (hit.collider == null || otherBeingSlot == null) return;

            if (!selectedSlot)
            {
                if (!otherBeingSlot.Being) return;
                selectedSlot = otherBeingSlot;
                selectedSlot.Being.OnSelect();
                Debug.Log("Selected!");
            }
            else
            {
                beingGrid.SwapBeings(selectedSlot, otherBeingSlot);
                otherBeingSlot.Being.OnDeselect();
                selectedSlot = null;
                Debug.Log("Swap!");
            }
        }
    }
}