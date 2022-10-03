using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XS_Utils;
//using Unity.EditorCoroutines.Editor; //Agafa aixo del Package [Editor Coroutines]

[CustomEditor(typeof(So))]
public class SoInpector : Editor
{
    public GUIStyle label70;
    public GUIStyle field;

    So so;

    AudioSource audioSource;
    AudioClip _nouClip = null;
    bool _loop;
    float _volumMin;
    float _volumMax;
    float _pitchMin;
    float _pitchMax;
    float _maxDist;
    bool spatialBlend;

    float testingDistance;

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        if(so == null)
        {
            so = (So)target;
        }

        if (label70 == null) label70 = new GUIStyle(GUI.skin.label) { fixedWidth = 40, alignment = TextAnchor.MiddleLeft };
        if (field == null) field = new GUIStyle(GUI.skin.textField) { fixedWidth = 40};

        /*
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
        _nouClip = (AudioClip)EditorGUILayout.ObjectField(_nouClip, typeof(AudioClip), false);
        if(_nouClip != null)
        {
            List<AudioClip> _tmp = new List<AudioClip>(so.clips);
            _tmp.Add(_nouClip);
            so.clips = _tmp.ToArray();
            _nouClip = null;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        */

        _loop = EditorGUILayout.Toggle("Loop", so.loop);
        if(so.loop != _loop)
        {
            Undo.RecordObject(so, "Guardar SonsPool desde SonsPoolInspector");
            so.loop = _loop;
        }
        EditorGUILayout.Space(20);


        EditorGUILayout.BeginHorizontal();
        _volumMin = so.volum.x;
        _volumMax = so.volum.y;
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
        _pitchMin = so.pitch.x;
        _pitchMax = so.pitch.y;
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

        testingDistance = EditorGUILayout.Slider("Testing distance",testingDistance, 0, 100);

        if (GUILayout.Button("Play"))
        {
            if (audioSource == null) audioSource = new GameObject("_preview").AddComponent<AudioSource>();
            int side = Random.Range(0, 2);
            audioSource.transform.position = Camera.main.transform.position + ( side == 1 ? Vector3.right : -Vector3.right) * testingDistance ;
            audioSource.clip = so.clips[Random.Range(0, so.clips.Length)];
            audioSource.loop = so.loop;
            audioSource.volume = Random.Range(so.volum.x, so.volum.y);
            audioSource.pitch = Random.Range(so.pitch.x, so.pitch.y);
            audioSource.spatialBlend = 0;
            audioSource.maxDistance = so.distanciaMaxima;
            audioSource.Play();
            Debugar.Log($"Play ({audioSource.clip.name})");
        }
        if (GUILayout.Button("Stop"))
        {
            if (audioSource == null) 
                return;

            if (!audioSource.isPlaying)
                return;

            audioSource.Stop();
        }

    }

    void OnDisable()
    {
        if (audioSource == null)
            return;

        DestroyImmediate(audioSource.gameObject);
    }

    IEnumerator ApagarSoProva(AudioSource _tmp)
    {
        yield return new WaitForSecondsRealtime(so.loop ? 3 : _tmp.clip.length);
        DestroyImmediate(_tmp.gameObject);
    }
}
