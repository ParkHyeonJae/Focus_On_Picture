using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCamera : global::CameraDevice
{
    [SerializeField] RawImage _screen;

    private void Awake()
    {
        FindObjectOfType<UIManager>().OnMissionPopup(true);
    }

    protected internal override void OnTakePicture(Transform target)
    {
        base.OnTakePicture(target);

        MissionManager.Instance.MoveNext();
        FindObjectOfType<UIManager>().OnMissionPopup(true);
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
