using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MusicaControlador : MonoBehaviour {

    const string UP = "(UP)";
    const string PLAY = "(PLAY)";
    const string DOWN = "(DOWN)";
    const string STOP = "(STOP)";

    public AudioSource audioSource;
    //public AudioClip clip;
    public Musica musica;

    public MusicaControlador[] altres;

    //[Range(0, 1)] public float volum;

    float activatComprovant;
    bool Iniciat
    {
        get
        {
            return musica.volum > 0;
        }
    }
    bool modificant = false;
    WaitForSeconds iniciarInteraccions;

    //public float volumMaxim = 1;
    //public bool playOnStart = false;

    private void Start()
    {
        iniciarInteraccions = new WaitForSeconds(0.001f);
        activatComprovant = musica.volum;

        //if (playOnStart) Play();

        ActualizarAltres();
        for (int i = 0; i < altres.Length; i++)
        {
            altres[i].ActualizarAltres();
        }
    }


    void Actualitzar()
    {
        //audioSource.volume = volum * activat;
        if (musica.volum <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        if (audioSource != null) audioSource.volume = musica.volumMaxim * musica.volum;
   
        if (!audioSource.isPlaying && Iniciat)
        {
            audioSource.Play();
            modificant = true;
        }
        else if (audioSource.isPlaying && !Iniciat)
        {
            audioSource.Stop();
            modificant = false;
        }

        if (musica.volum >= 1 && modificant) modificant = false;

        for (int i = 0; i < altres.Length; i++)
        {
            if (!altres[i].modificant && altres[i].musica.volum > 0)
            {
                altres[i].musica.volum = 1 - musica.volum;
                altres[i].Actualitzar();
            }
        }

        
    }

    void Update () {
        if(activatComprovant != musica.volum)
        {
            activatComprovant = musica.volum;
            Actualitzar();
        }
    }

    void CanviarPrefix(string _prefix)
    {
        if (!gameObject.name.StartsWith(_prefix))
        {
            int _indexInici = 0;

            if (gameObject.name.StartsWith(UP)) _indexInici = UP.Length;
            else if (gameObject.name.StartsWith(PLAY)) _indexInici = PLAY.Length;
            else if (gameObject.name.StartsWith(DOWN)) _indexInici = DOWN.Length;
            else if (gameObject.name.StartsWith(STOP)) _indexInici = STOP.Length;

            gameObject.name = _prefix + gameObject.name.Substring(_indexInici);
        }
    }

    [ContextMenu("Play")]
    public void Play()
    {
        StartCoroutine(Play_Temps());
    }

    public void ActualizarAltres()
    {
        MusicaControlador[] _altres = GameObject.FindObjectsOfType<MusicaControlador>();
        List<MusicaControlador> _tmp = new List<MusicaControlador>();
        if (_altres.Length > 1)
        {
            foreach (var item in _altres)
            {
                if (item != this && !_tmp.Contains(item))
                {
                    _tmp.Add(item);
                }
            }
            altres = _tmp.ToArray();
        }
    }

    public void Volum(float _volum)
    {
        musica.volum = _volum;
    }

    IEnumerator Play_Temps()
    {
        while(musica.volum < 1)
        {
            musica.volum += Time.deltaTime;
            yield return iniciarInteraccions;
        }
        musica.volum = 1;
        yield return null;
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        StartCoroutine(Stop_Temps());
    }

    IEnumerator Stop_Temps()
    {
        while (musica.volum > 0)
        {
            musica.volum -= Time.deltaTime;
            yield return iniciarInteraccions;
        }
        musica.volum = 0;
        yield return null;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < altres.Length; i++)
        {
            altres[i].altres = new MusicaControlador[] { };
            altres[i].ActualizarAltres();
        }
    }
}
