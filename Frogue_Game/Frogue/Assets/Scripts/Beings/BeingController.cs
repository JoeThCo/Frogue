using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingController : MonoBehaviour
{
    public Being Being { get; private set; }

    public void BeingControllerInit(Being being)
    {
        Being = being;
    }
}