using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Xido Studio/Audio/Mixers", fileName = "Mixers")]
public class Mixers : ScriptableObject {

    public static Mixers Instance;

    const string KEY_GUARDAT = "Audio_";
    const string KEY_MASTER = "VolumMaster";
    const string KEY_MUSICA = "VolumMusica";
    const string KEY_SO = "VolumSons";
    const string KEY_REVERB = "Reverberacio";
    const string KEY_ECO = "Eco";
    const string KEY_LOWPASSGENERAL = "LowPassGeneral";
    const string KEY_LOWPASSMUSICA = "LowPassMusica";

    [Informacio] [SerializeField] string Inportant = "S'ha d'Actualitzar, al inici perque aquest script afecti al Mixer.";
    public AudioMixer mixer;
    public AudioMixerGroup master;
    public AudioMixerGroup musica;
    public AudioMixerGroup sons;

    [Range(0, 1.25f)] public float volumMaster = 1;
    [Range(0, 1.25f)] public float volumMusica = 1;
    [Range(0, 1.25f)] public float volumSons = 1;

    [Range(0, 1)] public float reverberacio = 0;
    [Range(0, 1)] public float eco = 0;
    [Range(0, 1)] public float lowpassGeneral = 0;
    [Range(0, 1)] public float lowpassMusica = 0;

    public bool MixerActualitzat => Mixers.Instance.GetFloat(Mixers.Instance.master, KEY_MASTER) == Mixers.Instance.volumMaster;

    private void OnEnable()
    {
        Debug.Log("Enable");
        Instance = this;
        Actualitzar();
    }

    //SETTERS
    public void SetMaster(float _volum)
    {
        volumMaster = _volum;
        SetFloat(master, KEY_MASTER, -((volumMaster - 1) * -80));
    }
    public void SetMusica(float _volum)
    {
        volumMusica = _volum;
        SetFloat(musica, KEY_MUSICA, -((volumMusica - 1) * -80));
    }
    public void SetSo(float _volum)
    {
        volumSons = _volum;
        SetFloat(sons, KEY_SO, -((volumSons - 1) * -80));
    }

    public void SetReverberacio(float _valor)
    {
        reverberacio = _valor;
        SetFloat(master, KEY_REVERB, -10000 + (10000 * _valor));
    }
    public void SetEco(float _valor)
    {
        eco = _valor;
        SetFloat(sons, KEY_ECO, _valor / 20f);
    }

    public void SetLowPassGeneral(float _valor)
    {
        lowpassGeneral = _valor;
        SetFloat(master, KEY_LOWPASSGENERAL, 21990 - (21990 * _valor) + 10);
    }

    public void SetLowPassMusica(float _valor)
    {
        lowpassMusica = _valor;
        SetFloat(musica, KEY_LOWPASSMUSICA, 21990 - (21990 * _valor) + 10);
    }

    void SetFloat(AudioMixerGroup _mixer, string _key, float _valor)
    {
        _mixer.audioMixer.SetFloat(_key, _valor);
    }



   //GETTERS
   //float
    float GetReverberacio()
    {
        master.audioMixer.GetFloat(KEY_REVERB, out float _tmp);
        return (1 + (_tmp / 10000f));
    }
    float GetEco()
    {
        sons.audioMixer.GetFloat(KEY_ECO, out float _tmp);
        return _tmp * 20f;
    }
    float GetLowPass(AudioMixerGroup _mixer, string _key)
    {
        _mixer.audioMixer.GetFloat(_key, out float _tmp);
        return 1 - (_tmp / 22000f);
    }
    float GetFloat(AudioMixerGroup _mixer, string _key)
    {
        _mixer.audioMixer.GetFloat(_key, out float _tmp);
        return (_tmp / 80f) + 1;
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            XS_Utils.Debugar.Log("Validate");
            Actualitzar();
        }
    }

    public void Actualitzar()
    {
        XS_Utils.Debugar.Log("Actualitzar");
        if (Mixers.Instance.GetFloat(Mixers.Instance.master, KEY_MASTER) != Mixers.Instance.volumMaster)
        {
            //new Debug().Log($"Actualitzar Master");
        }
        if (Mixers.Instance.GetFloat(Mixers.Instance.musica, KEY_MUSICA) != Mixers.Instance.volumMusica)
        {
            //new Debug().Log($"Actualitzar Musica");
          
        }
        if (Mixers.Instance.GetFloat(Mixers.Instance.sons, KEY_SO) != Mixers.Instance.volumSons)
        {
            //new Debug().Log($"Actualitzar Sons");
        }
            Mixers.Instance.SetMaster(Mixers.Instance.volumMaster);
            Mixers.Instance.SetMusica(Mixers.Instance.volumMusica);
            Mixers.Instance.SetSo(Mixers.Instance.volumSons);


        if (Mixers.Instance.GetReverberacio() != Mixers.Instance.reverberacio)
        {
            //new Debug().Log($"Actualitzar Reververacio");
            Mixers.Instance.SetReverberacio(Mixers.Instance.reverberacio);
        }
        if (Mixers.Instance.GetEco() != Mixers.Instance.eco)
        {
            //new Debug().Log($"Actualitzar Eco: {Mixers.Instance.GetEco()} - {Mixers.Instance.eco}");
            Mixers.Instance.SetEco(Mixers.Instance.eco);
        }
        if (Mixers.Instance.GetLowPass(Mixers.Instance.master, KEY_LOWPASSGENERAL) != Mixers.Instance.lowpassGeneral)
        {
            //new Debug().Log($"Actualitzar LowpassGeneral");
            Mixers.Instance.SetLowPassGeneral(Mixers.Instance.lowpassGeneral);
        }
        if (Mixers.Instance.GetLowPass(Mixers.Instance.musica, KEY_LOWPASSMUSICA) != Mixers.Instance.lowpassMusica)
        {
            //new Debug().Log($"Actualitzar LowpassMusica");
            Mixers.Instance.SetLowPassMusica(Mixers.Instance.lowpassMusica);
        }
    }
}
