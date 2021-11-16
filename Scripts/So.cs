using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

[System.Serializable]
[CreateAssetMenu(menuName = "Xido Studio/Audio/So", fileName = "So")]
public class So : ScriptableObject
{
    [HideInInspector] public AudioClip defecte;
    [HideInInspector] public GameObject prefab;
    [HideInInspector] public GameObject pool;

    [HideInInspector] public bool loop;
    [HideInInspector] public Vector2 volum = new Vector2(1,1);
    [HideInInspector] public Vector2 pitch = new Vector2(1,1);
    [HideInInspector] public bool spatialBlend = true;
    [HideInInspector] public float distanciaMaxima = 100;

    public AudioClip[] clips = new AudioClip[] { };

    SoControlador actual;

    AudioClip clip;
    GameObject go;

    public SoControlador Play()
    {
        SetSo(null, false);
        return actual;
    }

    public SoControlador Play(Transform transform, bool emparentat = false) 
    {
        SetSo(transform, emparentat);
        return actual;
    }

    void SetSo(Transform transform, bool parent)
    {
        clip = clips.Length > 0 ? clips[Random.Range(0, clips.Length)] : defecte;
        //go = SonsPool.Get(clip != null ? clip.length : 0);
        //actual = go.GetComponent<SoControlador>();
        actual = SonsPoolAutomatic.Get(loop);
        //actual = _tmp.GetComponent<AudioSource>();

        actual.AudioSource.clip = clip;
        actual.AudioSource.loop = loop;
        actual.AudioSource.volume = Random.Range(volum.x, volum.y);
        actual.AudioSource.pitch = Random.Range(pitch.x, pitch.y);
        if (actual.AudioSource.outputAudioMixerGroup == null) actual.AudioSource.outputAudioMixerGroup = Mixers.Instance.sons;
        actual.AudioSource.spatialBlend = Application.isPlaying ? (spatialBlend ? 1 : 0) : 0;
        actual.AudioSource.maxDistance = distanciaMaxima;
        actual.AudioSource.Play();
        if(transform != null) actual.gameObject.transform.position = transform.position;
        actual.gameObject.transform.SetParent(parent ? transform : null);
    }


    public void Stop(AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.gameObject.SetActive(false);
    }

    public void Stop()
    {
        actual.AudioSource.Stop();
        actual.gameObject.SetActive(false);
    }

}
