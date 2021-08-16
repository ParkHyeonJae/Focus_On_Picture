using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class EnumGenerateEditor : EditorWindow
{

    private void OnEnable() => AssemblyReloadEvents.beforeAssemblyReload += AssemblyReloadEvents_afterAssemblyReload;
    private void OnDisable() => AssemblyReloadEvents.beforeAssemblyReload -= AssemblyReloadEvents_afterAssemblyReload;
    private void AssemblyReloadEvents_afterAssemblyReload() => TagGenerate();


    //private void Update() => Generate();

    [MenuItem("Editor/Generator/TagGenerate %g")]
    public static void TagGenerate()
    {
        EnumGenerator.EnumGenerator enumGenerator = new EnumGenerator.EnumGenerator();
        enumGenerator.filePath = Application.dataPath + "/Scripts/CodeGen/GenEnums.cs";
        enumGenerator.AddEnum("Tags", UnityEditorInternal.InternalEditorUtility.tags);
        enumGenerator.Generate();

        AssetDatabase.Refresh();
    }
}
