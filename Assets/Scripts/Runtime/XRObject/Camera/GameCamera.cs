using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCamera : global::CameraDevice
{
    [SerializeField] RawImage _screen;

    private void Awake()
    {
        UIManager.Instance.OnMissionPopup(true);
    }

    protected internal override void OnTakePicture(Transform target)
    {
        base.OnTakePicture(target);

        MissionManager.Instance.MoveNext();
        UIManager.Instance.OnMissionPopup(false);
        StartCoroutine(WaitUntilActiveMissionPopup(true));
    }

    IEnumerator WaitUntilActiveMissionPopup(bool active, float time = 1)
    {
        yield return new WaitForSeconds(time);
        UIManager.Instance.OnMissionPopup(active);
    }

    protected internal override void OnTryTakePicture()
    {
        base.OnTryTakePicture();

        StartCoroutine(Shutter());
    }

    IEnumerator Shutter()
    {
        var color = new Color(0, 0, 0, 0);
        _screen.color = color;

        while (color.a < 1.0f)
        {
            color.a += 0.01f;
            color.r += 0.01f;
            color.g += 0.01f;
            color.b += 0.01f;
            _screen.color = color;

            yield return null;
        }

        _screen.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        yield return null;
    }
}
