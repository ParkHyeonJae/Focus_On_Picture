using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Utils
{

    public static readonly string Path_ScreenShot = Application.dataPath + "/ScreenShot/";

    public static void ScreenShot(Camera targetCamera)
    {
        var directoryInfo = new DirectoryInfo(Path_ScreenShot);
        if (!directoryInfo.Exists)
            Directory.CreateDirectory(Path_ScreenShot);

        var saveFilePath = $"{System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.png";
        var renderTex = targetCamera.targetTexture;
        var outputTex = new Texture2D(renderTex.width, renderTex.height);
        targetCamera.Render();
        RenderTexture.active = renderTex;

        outputTex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        outputTex.Apply();

        var encodeBytes = outputTex.EncodeToPNG();
        File.WriteAllBytes(Path_ScreenShot + saveFilePath, encodeBytes);
    }
}
