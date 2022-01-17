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
    [HideInInspector] public bool spatialBlend = true;
    [HideInInspector] public float distanciaMaxima = 100;

    public AudioClip[] clips = new AudioClip[] { };

    SoControlador actual;

    AudioClip clip;
    GameObject go;

    public SoControlador Play() => Play(Set(Get(), Vector3.zero, null));
    public SoControlador Play(Transform transform) 
    {
        Get();
        SetBasics(actual, transform.position, transform);
        Play(actual);
        return actual;
    }
    public SoControlador Play(Vector3 position)
    {
        Get();
        SetBasics(actual, position, null);
        Play(actual);
        return actual;
    }
    public SoControlador Play(Vector3 position, Transform parent)
    {
        Get();
        SetBasics(actual, position, parent);
        Play(actual);
        return actual;
    }

    SoControlador Get() => actual = SonsPoolAutomatic.Get(loop);
    void SetBasics(SoControlador so, Vector3 position, Transform parent)
    {
        so.Clip = clip = clips.Length > 0 ? clips[Random.Range(0, clips.Length)] : defecte;
        so.Volume = Random.Range(volum.x, volum.y);
        so.Pitch = Random.Range(pitch.x, pitch.y);

        so.gameObject.transform.position = position;
        so.gameObject.transform.SetParent(parent);
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

    public SoControlador Play(SoControlador so) 
    {
        so.Loop = loop;
        so.AudioMixed();
        so.SpatialBlend = spatialBlend;
        so.MaxDistance = distanciaMaxima;
        so.Play();
        return so;
    } 


    public void Stop(AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.gameObject.SetActive(false);
    }

    public void Stop()
    {
        actual.Stop();
        actual.gameObject.SetActive(false);
    }

}
