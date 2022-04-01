using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XS_Utils;

public class MusicCreator
{
    [MenuItem("Assets/XidoSudio/Audio/Music")]
    static void CreateMusicFromAudioClip()
    {
        //Checking that you don't select wired assets.
        if (Selection.activeObject == null)
        {
            Debugar.LogError("Select and AudioClip to create a music");
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
        Music music = ScriptableObject.CreateInstance<Music>();
        music.AudioClip = (AudioClip)Selection.activeObject;

        //Create an asset on the project and save it.
        AssetDatabase.CreateAsset(music, $"{folder[0]}{name}.asset");
        AssetDatabase.SaveAssets();

        //Focus de asset created.
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = music;
    }
}
