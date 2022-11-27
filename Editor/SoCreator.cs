using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XS_Utils;

public class SoCreator
{
    [MenuItem("Assets/XidoStudio/Audio/So")]
    static void CreateSoFromAudioClip()
    {
        //Checking that you don't select wired assets.
        if(Selection.activeObject == null)
        {
            Debugar.LogError("Select and AudioClip to create a sound");
            return;
        }
        if (Selection.activeObject.GetType() != typeof(AudioClip))
        {
            Debugar.LogError("This object is not an AudioClip");
            return;
        }

        //Get the folder of the actual selected object.
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        string name = Selection.activeObject.name;
        string[] folder = path.Split(name);

        //Create Scriptable and sets it.
        So so = ScriptableObject.CreateInstance<So>();
        so.clips = new AudioClip[] { (AudioClip)Selection.activeObject };

        //Create an asset on the project and save it.
        AssetDatabase.CreateAsset(so, $"{folder[0]}{name}.asset");
        AssetDatabase.SaveAssets();

        //Focus de asset created.
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = so;
    }
}
