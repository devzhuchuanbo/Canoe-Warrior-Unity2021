#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 截取游戏图片，用于审核
/// </summary>
public class CaptureScreen
{
    [MenuItem("Tools/截屏", false)]
    private static void Capture()
    {
        var name = "截图_" + GetName() + ".png";
        ScreenCapture.CaptureScreenshot(name);
        Debug.Log("截图 " + name + "  ：在项目目录中可查看！");


        string GetName()
        {
            var name = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", System.DateTime.Now.Year.ToString(),
                                                                System.DateTime.Now.Month.ToString(),
                                                                System.DateTime.Now.Day.ToString(),
                                                                System.DateTime.Now.Hour.ToString(),
                                                                System.DateTime.Now.Minute.ToString(),
                                                                System.DateTime.Now.Second.ToString());
            return name;

        }
    }
}
#endif