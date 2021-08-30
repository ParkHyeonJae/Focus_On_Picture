using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;
using System;

[RequireComponent(typeof(XRUser))]
public class XRMover : MonoBehaviour
{
    public enum EUserState
    {
        /// <summary>
        /// 정지 상태
        /// </summary>
        IDLE = 0,
        /// <summary>
        /// 이동 상태
        /// </summary>
        MOVE = 1,
    }


    [Range(0.1f, 100.0f)]
    [SerializeField] float _moveSpeed = 10f;

    public EUserState eUserState { get; set; } = EUserState.IDLE;
    public event Action<EUserState> onUserStateChanged;

    private XRUser _user;
    private InputAction _lAction;
    private InputAction _rAction;

    Vector3 _moveDir;

    CharacterController characterController;

    private void Awake()
    {
        _user = GameManager.Instance.User;
        _lAction = _user.InputActionManager.actionAssets[0].actionMaps[1].actions[10];
        _rAction = _user.InputActionManager.actionAssets[0].actionMaps[2].actions[10];


        characterController = GetComponent<CharacterController>();
        
        //characterController.attachedRigidbody.useGravity = true;
    }

    
    private void Update()
    {
        if (eUserState == EUserState.IDLE)
            return;

        _moveDir = _user.MainCamera.transform.rotation * Vector3.forward * 1;


        _moveDir.y -= 20 * Time.deltaTime;

        if (_moveDir.y > 0)
            _moveDir.y = 0;



        if (characterController)
            characterController.Move(_moveDir * Time.deltaTime);

    }


    public void StartMove()
    {
        eUserState = EUserState.MOVE;
        _moveDir = _user.MainCamera.transform.rotation * Vector3.forward * 1;
        DOTween.To(() => new Vector3(0,0,0), x => characterController.Move(x * Time.deltaTime), _moveDir, 1.0f);

        onUserStateChanged?.Invoke(eUserState);
    }
    public void StopMove()
    {
        eUserState = EUserState.IDLE;

        DOTween.To(() => _moveDir, x => characterController.Move(x * Time.deltaTime), new Vector3(0, 0, 0), 1.0f);

        onUserStateChanged?.Invoke(eUserState);
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
