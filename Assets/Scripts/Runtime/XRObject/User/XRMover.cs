using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(XRUser))]
public class XRMover : MonoBehaviour
{
    [Range(0.1f, 100.0f)]
    [SerializeField] float _moveSpeed = 10f;

    private XRUser _user;
    private InputAction _lAction;
    private InputAction _rAction;

    private void Awake()
    {
        _user = GameManager.Instance.User;
        _lAction = _user.InputActionManager.actionAssets[0].actionMaps[1].actions[10];
        _rAction = _user.InputActionManager.actionAssets[0].actionMaps[2].actions[10];
    }

    private void OnEnable()
    {
        _lAction.performed += LAction_performed;
        _rAction.performed += RAction_performed;
    }

    private void OnDisable()
    {
        _lAction.performed -= LAction_performed;
        _rAction.performed -= RAction_performed;
    }

    private void LAction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var pos = obj.ReadValue<Vector2>();

        //_user.RigidBody.AddForce(new Vector3(pos.x, 0, pos.y) * _moveSpeed, ForceMode.Force);
        transform.Translate(new Vector3(pos.x, 0, pos.y) * Time.deltaTime * _moveSpeed, Space.World);
    }
    private void RAction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var pos = obj.ReadValue<Vector2>();

        //_user.RigidBody.AddForce(new Vector3(pos.x, 0, pos.y) * _moveSpeed, ForceMode.Force);
        transform.Translate(new Vector3(pos.x, 0, pos.y) * Time.deltaTime * _moveSpeed, Space.World);
    }
}
