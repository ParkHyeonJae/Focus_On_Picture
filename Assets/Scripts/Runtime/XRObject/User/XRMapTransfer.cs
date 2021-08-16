using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class XRMapTransfer : MonoBehaviour
{
    private XRUser _user;
    private InputAction _lAction;
    private InputAction _rAction;

    static int _worldSceneIndex = 0;

    private void Awake()
    {
        _user = GameManager.Instance.User;
        _lAction = _user.InputActionManager.actionAssets[0].actionMaps[1].actions[3];
        _rAction = _user.InputActionManager.actionAssets[0].actionMaps[2].actions[3];
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

    private void LAction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => ChangeWorld();
    private void RAction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => ChangeWorld();

    private void ChangeWorld()
    {
        var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
    }

}
