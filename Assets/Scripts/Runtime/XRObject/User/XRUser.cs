using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class XRUser : MonoBehaviour
{
    private XRRig _xRRig;
    public XRRig XR_Rig
        => _xRRig = _xRRig ?? GetComponentInChildren<XRRig>();

    private XRMover _xRMover;
    public XRMover XRMover
        => _xRMover = _xRMover ?? GetComponentInChildren<XRMover>();


    private InputActionManager _inputActionManager;
    public InputActionManager InputActionManager
        => _inputActionManager = _inputActionManager ?? GetComponentInChildren<InputActionManager>();


    private Camera _mainCamera;
    public Camera MainCamera
        => _mainCamera = _mainCamera ?? Camera.main;

    private Rigidbody _rigidbody;
    public Rigidbody RigidBody
        => _rigidbody = _rigidbody ?? GetComponent<Rigidbody>();

    private XRUserInteractaor _xRUserInteractaor;
    public XRUserInteractaor XRUserInteractaor
        => _xRUserInteractaor = _xRUserInteractaor ?? GetComponent<XRUserInteractaor>();

}
