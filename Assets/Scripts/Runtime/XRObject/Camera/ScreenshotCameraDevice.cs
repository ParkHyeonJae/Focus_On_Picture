using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenshotCameraDevice : global::CameraDevice
{
    protected internal override void OnTakePicture(Transform target)
    {
        base.OnTakePicture(target);

        global::Utils.ScreenShot(this.viewCamera);
    }
}
