using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/Audio/Musica", fileName = "Musica")]
public class Musica : ScriptableObject
{
    [HideInInspector] public GameObject prefab;

    public AudioClip clip;
    MusicaControlador musica;

    [HideInInspector] public float volum = 0;
    [Range(0, 1)] public float volumMaxim;
    //public bool playOnStart;

    public void Volum(float _volum)
    {
        volum = _volum;
        if(volum > 0)
        {
            if (musica == null)
            {
                CrearMusica();
            }
            else
            {

            }
        }
        else
        {
            if(musica != null)
            {
                Destroy(musica.gameObject);
                musica = null;
            }
        }    
    }
    public void Play()
    {
        if (musica != null)
            return;

        CrearMusica();
        musica.Play();
    }
    public void Stop()
    {
        if (musica == null)
            return;

        musica.Stop();
    }

    void CrearMusica()
    {
        GameObject _tmp = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        _tmp.name = $"_Musica({this.name})";
        MusicaControlador _musica = _tmp.GetComponent<MusicaControlador>();
        _musica.musica = this;
        _musica.audioSource.clip = clip;
        musica = _musica;
    }
}
