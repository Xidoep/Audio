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

    [HideInInspector] public Vector2 volum = new Vector2(1,1);
    [HideInInspector] public Vector2 pitch = new Vector2(1,1);

    [HideInInspector] public bool loop;
    [HideInInspector] public float distanciaMaxima = 500;

    public AudioClip[] clips = new AudioClip[] { };

    SoControlador anteriorControlador;



    public SoControlador Play() => Play(Set(Get, Vector3.zero, null));
    public SoControlador Play(float delay) => Play(Set(Get, Vector3.zero, null), delay);
    public SoControlador Play(float volume, float pitch) => Play(SetAll(Get, volume, pitch, Vector3.zero, null));
    public SoControlador Play(Transform transform) => Play(Set(Get, transform.position, transform));
    public SoControlador Play(Transform transform, float delay) => Play(Set(Get, transform.position, transform), delay);
    public SoControlador Play(Vector3 position) => Play(Set(Get, position, null));
    public SoControlador Play(Vector3 position, float volume) => Play(SetWithVolume(Get, volume, position, null));
    public SoControlador Play(Vector3 position, float volume, float pitch) => Play(SetAll(Get, volume, pitch, position, null));
    public SoControlador Play(Vector3 position, Transform parent) => Play(Set(Get, position, parent));

    SoControlador Get => anteriorControlador = SonsPoolAutomatic.Get(loop);
    void SetBasics(SoControlador so, Vector3 position, Transform parent)
    {
        so.Clip = clips.Length > 0 ? clips[Random.Range(0, clips.Length)] : defecte;
        so.Volume = Random.Range(volum.x, volum.y);
        so.Pitch = Random.Range(pitch.x, pitch.y);
        so.Loop = loop;
        so.SpatialBlend = position == Vector3.zero ? 0 : 1;
        so.MaxDistance = distanciaMaxima;

        so.gameObject.transform.position = position;
        so.gameObject.transform.SetParent(parent);

        so.AudioMixed();
    }
    SoControlador Set(SoControlador so, Vector3 position, Transform parent)
    {
        SetBasics(so, position, parent);
        return so;
    }
    SoControlador SetWithVolume(SoControlador so, float volume, Vector3 position, Transform parent)
    {
        SetBasics(so, position, parent);
        so.Volume = volume;
        return so;
    }
    SoControlador SetWithPitch(SoControlador so, float pitch, Vector3 position, Transform parent)
    {
        SetBasics(so, position, parent);
        so.Pitch = pitch;
        return so;
    }
    SoControlador SetAll(SoControlador so, float volume, float pitch, Vector3 position, Transform parent)
    {
        SetBasics(so, position, parent);
        so.Volume = volume;
        so.Pitch = pitch;
        return so;
    }

    SoControlador Play(SoControlador so) 
    {
        so.Play();
        return so;
    }
    SoControlador Play(SoControlador so, float delay)
    {
        so.Play(delay);
        return so;
    }

    public void Stop(AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.gameObject.SetActive(false);
    }

    public void Stop()
    {
        anteriorControlador.Stop();
        anteriorControlador.gameObject.SetActive(false);
    }

}
