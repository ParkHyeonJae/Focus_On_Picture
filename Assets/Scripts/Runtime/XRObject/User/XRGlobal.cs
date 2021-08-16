using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRGlobal : MonoBehaviour
{
    bool Initalize = false;
    private void Awake()
    {
        if (!Initalize)
        {
            DontDestroyOnLoad(gameObject);
            Initalize = true;
        }
    }
}
