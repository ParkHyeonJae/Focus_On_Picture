using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// VR Trigger 관련 조작 스크립트
/// </summary>
public class ObjectActivator : MonoBehaviour
{
    [Header("Activated Trigger")]
    public UnityEvent onActivated;

    [Header("Deactivated Trigger")]
    public UnityEvent onDeactivated;

    bool m_Activated = false;

    public void Activated()
    {
        if (!m_Activated)
        {
            onActivated?.Invoke();

            m_Activated = true;
        }
        else
        {
            onDeactivated?.Invoke();
            m_Activated = false;
        }
    }


    public void Deactivated()
    {

        onDeactivated?.Invoke();
        m_Activated = false;

    }

}
