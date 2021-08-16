using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    protected Rigidbody XR_Rigidbody
        => _rigidbody = _rigidbody ?? GetComponentInChildren<Rigidbody>();

    private MeshRenderer _meshRenderer;
    protected MeshRenderer XR_MeshRenderer
        => _meshRenderer = _meshRenderer ?? GetComponentInChildren<MeshRenderer>();

    private MeshCollider _meshCollider;
    protected MeshCollider XR_MeshCollider
        => _meshCollider = _meshCollider ?? GetComponentInChildren<MeshCollider>();

    private ObjectActivator _objectActivator;
    protected ObjectActivator XR_ObjectActivator
        => _objectActivator = _objectActivator ?? GetComponentInChildren<ObjectActivator>();

    private Transform _attachTransform;
    protected Transform XR_AttachTransform
        => _attachTransform = _attachTransform ?? transform.Find("Attach");

    private XRGrabInteractable _xRGrabInteractable;
    protected XRGrabInteractable XR_GrabInteractable
        => _xRGrabInteractable = _xRGrabInteractable ?? GetComponentInChildren<XRGrabInteractable>();
}
