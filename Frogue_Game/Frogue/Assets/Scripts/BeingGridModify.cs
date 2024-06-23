using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingGridModify : MonoBehaviour
{
    [SerializeField] private BeingGrid beingGrid;
    [SerializeField] private BeingSlot selectedBeing;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            BeingSlot otherBeingSlot = hit.collider.gameObject.GetComponent<BeingSlot>();

            if (hit.collider != null && otherBeingSlot)
            {
                if (!selectedBeing)
                {
                    Debug.Log("Selected!");
                    selectedBeing = otherBeingSlot;
                }
                else
                {
                    Debug.Log("Swap!");
                    beingGrid.SwapBeings(selectedBeing, otherBeingSlot);
                    selectedBeing = null;
                }
            }
        }
    }
}