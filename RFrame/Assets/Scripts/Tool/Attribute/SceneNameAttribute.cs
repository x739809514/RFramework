using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneNameAttribute : PropertyAttribute
{
    public string[] NameList => AllSceneNames();

    public static string[] AllSceneNames()
    {
        var stringList = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                var str1 = scene.path.Substring(scene.path.LastIndexOf("/") + 1);
                var str2 = str1.Substring(0, str1.Length - 6);
                stringList.Add(str2);
            }
        }

        return stringList.ToArray();
    }
}