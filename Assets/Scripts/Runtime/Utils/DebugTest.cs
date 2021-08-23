using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DebugTest : MonoBehaviour
{
    public void Select_performed()
    {
        Debug.Log("Triggered");
    }
    public void OnHoverEnter(XRBaseInteractable xRBaseInteractable)
    {
        Debug.Log("Enter Hover");
    }

    public void OnHoverExit(XRBaseInteractable xRBaseInteractable)
    {
        Debug.Log("Exit Hover");
    }
}
