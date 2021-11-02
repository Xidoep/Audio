using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Musica))]
public class MusicaInspector : Editor
{
    GUIStyle _verd = null;
    GUIStyle _groc = null;
    GUIStyle _vermell = null;

    public override void OnInspectorGUI()
    {
        if (_verd == null) _verd = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.LowerCenter, normal = new GUIStyleState() { textColor = Color.green } };
        if (_groc == null) _groc = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.LowerCenter, normal = new GUIStyleState() { textColor = Color.yellow } };
        if (_vermell == null) _vermell = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.LowerCenter, normal = new GUIStyleState() { textColor = Color.red } };

        base.OnInspectorGUI();

        Musica _musica = (Musica)target;

        /*if (!_musica.audioSource)
        {
            AudioSource _audioSource = _musica.GetComponent<AudioSource>();
            if (_audioSource)
            {
                _musica.audioSource = _audioSource;
            }
            else
            {
                Debug.Log("Falta AudioSource");
            }
        }

        if (!_musica.audioSource)
            return;
*/
        /*_musica.clip = (AudioClip)EditorGUILayout.ObjectField(_musica.clip, typeof(AudioClip), true);
        if (_musica.audioSource.clip != _musica.clip)
        {
            _musica.audioSource.clip = _musica.clip;
            _musica.gameObject.name = $"_Musica({ _musica.audioSource.clip.name})";
        }*/

        //if (!_musica.audioSource.clip)
        //    return;
        //_musica.volum = EditorGUILayout.Slider(_musica.volum, 0, 1);
        //_musica.playOnStart = EditorGUILayout.Toggle("Play On Start", _musica.playOnStart);
        /*float _volum = EditorGUILayout.Slider("Volum", _musica.volumMaxim, 0, 1);
        if(_musica.volumMaxim != _volum)
        {
            _musica.volumMaxim = _volum;
            _musica.audioSource.volume = _volum;
        }*/
        

        if(_musica.volum > 0)
        {
            if (GUILayout.Button("STOP"))
            {
                _musica.Stop();
            }
        }
        else
        {
            if (GUILayout.Button("PLAY"))
            {
                _musica.Play();
            }
        }
        
    }
}
