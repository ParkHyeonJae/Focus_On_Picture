using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorManager : MonoBehaviour
{
    private static InteractorManager _instance;
    public static InteractorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InteractorManager>();

                if (_instance == null)
                    _instance = new GameObject($"@{nameof(InteractorManager)}").AddComponent<InteractorManager>();

            }
            return _instance;
        }
    }


    private XRUserInteractaor _xRUserInteractaor;

    public XRUserInteractaor XRUserInteractaor
        => _xRUserInteractaor = _xRUserInteractaor ?? GameManager.Instance.User.XRUserInteractaor;

    private UIManager _uIManager;

    private void Awake()
    {
        _uIManager = FindObjectOfType<UIManager>();

        XRUserInteractaor.OnActiveInteraction += new System.Action<UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable>((interactable) => { _uIManager.OnInteraction(OnInteractCompleted); });
        XRUserInteractaor.OnDeactiveInteraction += new System.Action<UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable>((interactable) => { _uIManager.OffInteraction(); });
    }

    void OnInteractCompleted()
    {
        //TODO
        Debug.Log("Interaction Completed");
    }
}
