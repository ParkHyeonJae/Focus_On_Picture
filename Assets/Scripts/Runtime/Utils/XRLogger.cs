using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XRLogger : MonoBehaviour
{
    string myLog;
    Queue myLogQueue = new Queue();

    private TextMeshProUGUI _text = null;
    public TextMeshProUGUI text => _text = _text ?? GetComponent<TextMeshProUGUI>(); 

    void Start()
    {
        Debug.Log("Log1");
        Debug.Log("Log2");
        Debug.Log("Log3");
        Debug.Log("Log4");
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }

        text.text = myLog;
    }
}
