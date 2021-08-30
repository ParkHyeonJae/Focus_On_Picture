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
        _uIManager = UIManager.Instance;

        SetActiveArrowUI(false);

        XRUserInteractaor.OnActiveInteraction += new System.Action<UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable>((interactable) => 
        {
            _uIManager._interaction.transform.position = Vector3.Lerp(Camera.main.transform.position, interactable.transform.position, 0.8f);
            _uIManager._interaction.transform.LookAt(Camera.main.transform);
            _uIManager._interaction.transform.Rotate(new Vector2(0, 180));

            //_uIManager._interaction.transform.position = Camera.main.WorldToScreenPoint(interactable.transform.position);
            _uIManager.OnInteraction(OnInteractCompleted);
        });
        XRUserInteractaor.OnDeactiveInteraction += new System.Action<UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable>((interactable) => { _uIManager.OffInteraction(); });


        GameManager.Instance.User.XRMover.onUserStateChanged += XRMover_onUserStateChanged;
        MissionManager.Instance.onMoveNextMissionCallback += Instance_onMoveNextMissionCallback;

    }

    private void Instance_onMoveNextMissionCallback(Mission prev, Mission next)
    {
        SetActiveArrowUI(true);
    }

    private void XRMover_onUserStateChanged(XRMover.EUserState obj)
    {
        switch (obj)
        {
            case XRMover.EUserState.IDLE:
                SetActiveArrowUI(false);
                break;
            case XRMover.EUserState.MOVE:
                SetActiveArrowUI(true);
                break;
            default:
                break;
        }
    }

    void SetActiveArrowUI(bool active)
    {
        _uIManager._distance.gameObject.SetActive(active);
        _uIManager._compass.gameObject.SetActive(active);
    }

    void OnInteractCompleted()
    {
        //TODO
        Debug.Log("Interaction Completed");
        GameManager.Instance.User.XRMover.StartMove();
    }
}
