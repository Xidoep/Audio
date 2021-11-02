using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor; //Agafa aixo del Package [Editor Coroutines]

[CustomEditor(typeof(So))]
public class SoInpector : Editor
{
    public GUIStyle label70;
    public GUIStyle field;

    So so;

    AudioSource audioSource;

    public override void OnInspectorGUI()
    {
        if(so == null)
        {
            so = (So)target;
        }

        if (label70 == null) label70 = new GUIStyle(GUI.skin.label) { fixedWidth = 40, alignment = TextAnchor.MiddleLeft };
        if (field == null) field = new GUIStyle(GUI.skin.textField) { fixedWidth = 40};

        for (int i = 0; i < so.clips.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();

            AudioClip _clip = (AudioClip)EditorGUILayout.ObjectField(so.clips[i], typeof(AudioClip), false);

            if (so.clips[i] != _clip)
            {
                Undo.RecordObject(so, "Guardar Sons desde SonsPoolInspector");
                so.clips[i] = _clip;
            }
            if (GUILayout.Button("X"))
            {
                Undo.RecordObject(so, "Guardar SonsPool desde SonsPoolInspector");
                List<AudioClip> _tmp = new List<AudioClip>(so.clips);
                _tmp.RemoveAt(i);
                so.clips = _tmp.ToArray();
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Box("+", label70);
        AudioClip _nouClip = null;
        _nouClip = (AudioClip)EditorGUILayout.ObjectField(_nouClip, typeof(AudioClip), false);
        if(_nouClip != null)
        {
            List<AudioClip> _tmp = new List<AudioClip>(so.clips);
            _tmp.Add(_nouClip);
            so.clips = _tmp.ToArray();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        bool _loop = EditorGUILayout.Toggle("Loop", so.loop);
        if(so.loop != _loop)
        {
            Undo.RecordObject(so, "Guardar SonsPool desde SonsPoolInspector");
            so.loop = _loop;
        }
        EditorGUILayout.Space(20);


        EditorGUILayout.BeginHorizontal();
        float _volumMin = so.volum.x;
        float _volumMax = so.volum.y;
        GUILayout.Box("Volum", label70);
        EditorGUILayout.MinMaxSlider(ref _volumMin, ref _volumMax, 0, 1);
        if(so.volum.x != _volumMin || so.volum.y != _volumMax)
        {
            Undo.RecordObject(so, "Guardar SonsPool desde SonsPoolInspector");
            so.volum = new Vector2(_volumMin, _volumMax);
        }
        EditorGUILayout.EndHorizontal(); 
        Vector2 _volum = EditorGUILayout.Vector2Field("", so.volum);
        if(so.volum != _volum)
        {
            Undo.RecordObject(so, "Guardar SonsPool desde SonsPoolInspector");
            so.volum = _volum;
        }
        EditorGUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        float _pitchMin = so.pitch.x;
        float _pitchMax = so.pitch.y;
        GUILayout.Box("Pitch", label70);
        EditorGUILayout.MinMaxSlider("", ref _pitchMin, ref _pitchMax, 0, 2);
        if (so.pitch.x != _pitchMin || so.pitch.y != _pitchMax)
        {
            Undo.RecordObject(so, "Guardar SonsPool desde SonsPoolInspector");
            so.pitch = new Vector2(_pitchMin, _pitchMax);
        }
        EditorGUILayout.EndHorizontal();
        Vector2 _pitch = EditorGUILayout.Vector2Field("", so.pitch);
        if (so.pitch != _pitch)
        {
            Undo.RecordObject(so, "Guardar SonsPool desde SonsPoolInspector");
            so.pitch = _pitch;
        }
        EditorGUILayout.Space(20);



        float _maxDist = EditorGUILayout.FloatField("Distancia maxima", so.distanciaMaxima);
        if (so.distanciaMaxima != _maxDist)
        {
            Undo.RecordObject(so, "Guardar SonsPool desde SonsPoolInspector");
            so.distanciaMaxima = _maxDist;
        }
        EditorGUILayout.Space(20);


        if (GUILayout.Button("Play"))
        {
            //GameObject _tmp = new GameObject();
            so.Play();
            //DestroyImmediate(_tmp);
            //EditorCoroutineUtility.StartCoroutine(ApagarSoProva(_ass), so);
        }
        if (GUILayout.Button("Stop"))
        {
            so.Stop();
            //EditorCoroutineUtility.StartCoroutine(ApagarSoProva(_ass), so);
        }
    }

    IEnumerator ApagarSoProva(AudioSource _tmp)
    {
        yield return new WaitForSecondsRealtime(so.loop ? 3 : _tmp.clip.length);
        DestroyImmediate(_tmp.gameObject);
    }
}
