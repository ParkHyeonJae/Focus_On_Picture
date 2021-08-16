using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                    _instance = new GameObject($"@{nameof(GameManager)}").AddComponent<GameManager>();

            }
            return _instance;
        }
    }

    private XRUser _xRUser;
    public XRUser User
        => _xRUser = _xRUser ?? FindObjectOfType<XRUser>();
}
