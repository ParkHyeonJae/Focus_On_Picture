using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDevice : global::XRObject
{
    private Camera _viewCamera = null;

    [HideInInspector]
    public Camera viewCamera
        => _viewCamera = _viewCamera ?? GetComponentInChildren<Camera>();


    public bool IsExistInsideScreenByObject(Transform target)
    {
        var viewPoint = viewCamera.WorldToViewportPoint(target.position);
        Debug.Log("view point : " + viewPoint);
        if (!(0 <= viewPoint.x && viewPoint.x <= 1.0f
            && 0 <= viewPoint.y && viewPoint.y <= 1.0f))
            return false;
        return true;
    }

    public void TakePicture()
    {
        Debug.Log("Take a Picture");

        OnTryTakePicture();

        var missionObjects = MissionManager.Instance.GetTargets;
        if (missionObjects.Length == 0)
            return;

        foreach (var target in missionObjects)
        {
            if (!IsExistInsideScreenByObject(target.transform))
                continue;

            Debug.Log($"Got It! : {target.name}");
            OnTakePicture(target.transform);
            break;
        }
    }

    protected internal virtual void OnTryTakePicture()
    {

    }

    protected internal virtual void OnTakePicture(Transform target)
    {
        
    }
}
