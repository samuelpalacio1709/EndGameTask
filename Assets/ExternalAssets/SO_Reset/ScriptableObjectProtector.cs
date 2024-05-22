//External asset by https://gist.github.com/yCatDev/d54c6c4d757adb5df38b3e53e4d0f954 to reset all ScriptableObjects to original state 

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class ScriptableObjectProtector
{
    private static bool _playing = false;

    static ScriptableObjectProtector()
    {
        EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode && !_playing)
        {
            foreach (var scriptableObject in FindScriptableObjectsByType())
            {
                EditorUtility.SetDirty(scriptableObject);
                Undo.RegisterCompleteObjectUndo(scriptableObject, scriptableObject.name);
            }
            _playing = true;
        }

        if (!EditorApplication.isPlayingOrWillChangePlaymode && _playing)
        {
            //Debug.Log("Revering ScriptableObjects");
            _playing = false;
            Undo.PerformUndo();
        }
    }

    private static List<ScriptableObject> FindScriptableObjectsByType()
    {
        var assets = new List<ScriptableObject>();
        var guids = AssetDatabase.FindAssets($"t:{typeof(ScriptableObject)}");
        foreach (var t in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(t);
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }

        return assets;
    }
}
#endif