using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class XRUserInteractaor : MonoBehaviour
{
    public event Action<XRBaseInteractable> OnActiveInteraction;

    public event Action<XRBaseInteractable> OnDeactiveInteraction;

    public void OnEnterHover(XRBaseInteractable xRBaseInteractable)
    {
        _OnActiveInteraction(xRBaseInteractable);
    }

    public void OnExitHover(XRBaseInteractable xRBaseInteractable)
    {
        _OnDeactiveInteraction(xRBaseInteractable);
    }

    public void _OnActiveInteraction(XRBaseInteractable xRBaseInteractable)
    {
        OnActiveInteraction?.Invoke(xRBaseInteractable);
    }

    public void _OnDeactiveInteraction(XRBaseInteractable xRBaseInteractable)
    {
        OnDeactiveInteraction?.Invoke(xRBaseInteractable);
    }

}
