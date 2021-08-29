using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

[RequireComponent(typeof(XRUser))]
public class XRMover : MonoBehaviour
{
    [Range(0.1f, 100.0f)]
    [SerializeField] float _moveSpeed = 10f;

    private XRUser _user;
    private InputAction _lAction;
    private InputAction _rAction;

    bool _isMove = false;

    CharacterController characterController;

    private void Awake()
    {
        _user = GameManager.Instance.User;
        _lAction = _user.InputActionManager.actionAssets[0].actionMaps[1].actions[10];
        _rAction = _user.InputActionManager.actionAssets[0].actionMaps[2].actions[10];


        characterController = GetComponent<CharacterController>();
        StartMove();
        //characterController.attachedRigidbody.useGravity = true;
    }

    Vector3 moveDir;
    private void Update()
    {

        if (!_isMove)
        {
            return;
        }

        moveDir = _user.MainCamera.transform.rotation * Vector3.forward * 1;


        moveDir.y -= 20 * Time.deltaTime;

        if (moveDir.y > 0)
            moveDir.y = 0;



        if (characterController)
        {
            var coll = characterController.Move(moveDir * Time.deltaTime);
        }

    }


    public void StartMove()
    {
        _isMove = true;

        moveDir = _user.MainCamera.transform.rotation * Vector3.forward * 1;
        DOTween.To(() => new Vector3(0,0,0), x => characterController.Move(x * Time.deltaTime), moveDir, 1.0f);


    }
    public void StopMove()
    {
        DOTween.To(() => moveDir, x => characterController.Move(x * Time.deltaTime), new Vector3(0, 0, 0), 1.0f);

        _isMove = false;
    }


    //private void OnEnable()
    //{
    //    _lAction.performed += LAction_performed;
    //    _rAction.performed += RAction_performed;
    //}

    //private void OnDisable()
    //{
    //    _lAction.performed -= LAction_performed;
    //    _rAction.performed -= RAction_performed;
    //}

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
