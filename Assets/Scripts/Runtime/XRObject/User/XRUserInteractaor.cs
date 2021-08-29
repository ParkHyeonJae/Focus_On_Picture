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
        if (MissionManager.Instance.missionTags.Contains(xRBaseInteractable.tag) && xRBaseInteractable.GetComponentInChildren<XRSign>() != null)
            _OnActiveInteraction(xRBaseInteractable);
    }

    public void OnExitHover(XRBaseInteractable xRBaseInteractable)
    {
        if (MissionManager.Instance.missionTags.Contains(xRBaseInteractable.tag) && xRBaseInteractable.GetComponentInChildren<XRSign>() != null)
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
