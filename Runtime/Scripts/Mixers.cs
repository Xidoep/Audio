using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Audio/Mixers", fileName = "Mixers")]
public class Mixers : ScriptableObject 
{
    public readonly string KEY_MASTER = "VolumMaster";
    public readonly string KEY_MUSICA = "VolumMusica";
    public readonly string KEY_SO = "VolumSons";
    public readonly string KEY_REVERB = "Reverberacio";
    public readonly string KEY_ECO = "Eco";
    public readonly string KEY_LOWPASSGENERAL = "LowPassGeneral";
    public readonly string KEY_LOWPASSMUSICA = "LowPassMusica";

    public static Mixers Instance;

    [SerializeField] Guardat guardat;

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioMixerGroup master;
    [SerializeField] AudioMixerGroup musica;
    [SerializeField] AudioMixerGroup sons;

    [Range(0, 1.25f)] [SerializeField] float volumMaster = 1;
    [Range(0, 1.25f)] [SerializeField] float volumMusica = 1;
    [Range(0, 1.25f)] [SerializeField] float volumSons = 1;

    [Range(0, 1)] [SerializeField] float reverberacio = 0;
    [Range(0, 1)] [SerializeField] float eco = 0;
    [Range(0, 1)] [SerializeField] float lowpassGeneral = 0;
    [Range(0, 1)] [SerializeField] float lowpassMusica = 0;

    public AudioMixerGroup Sons => sons;

    //public bool MixerActualitzat => Mixers.Instance.GetFloat(Mixers.Instance.master, KEY_MASTER) == Mixers.Instance.volumMaster;

    private void OnEnable()
    {
        Debugar.Log("[Mixers] OnEnable() => Instance, Carregar(), Actualitzar() i guardat.onLoad += Carregar");
        Instance = this;
        Carregar();
        //Actualitzar();
        guardat.onLoad += Carregar;
    }

    private void OnDisable()
    {
        guardat.onLoad -= Carregar;
    }

    public void Carregar()
    {
        SetMaster((float)guardat.Get(KEY_MASTER, 1f));
        SetMusica((float)guardat.Get(KEY_MUSICA, 1f));
        SetSo((float)guardat.Get(KEY_SO, 1f));
        SetReverberacio((float)guardat.Get(KEY_REVERB, 0));
        SetEco((float)guardat.Get(KEY_ECO, 0));
        SetLowPassGeneral((float)guardat.Get(KEY_LOWPASSGENERAL, 0));
        SetLowPassMusica((float)guardat.Get(KEY_LOWPASSMUSICA, 0));

        //Actualitzar();
    }

    //SETTERS
    public void SetMaster(float _volum)
    {
        volumMaster = _volum;
        SetFloat(master, KEY_MASTER, -((volumMaster - 1) * -80));
        guardat.SetLocal(KEY_MASTER, volumMaster);
    }
    public void SetMusica(float _volum)
    {
        volumMusica = _volum;
        SetFloat(musica, KEY_MUSICA, -((volumMusica - 1) * -80));
        guardat.SetLocal(KEY_MUSICA, volumMusica);
    }
    public void SetSo(float _volum)
    {
        volumSons = _volum;
        SetFloat(sons, KEY_SO, -((volumSons - 1) * -80));
        guardat.SetLocal(KEY_SO, volumSons);
    }

    public void SetReverberacio(float _valor)
    {
        reverberacio = _valor;
        SetFloat(master, KEY_REVERB, -10000 + (10000 * _valor));
        guardat.SetLocal(KEY_REVERB, reverberacio);
    }
    public void SetEco(float _valor)
    {
        eco = _valor;
        SetFloat(sons, KEY_ECO, _valor / 20f);
        guardat.SetLocal(KEY_ECO, eco);
    }

    public void SetLowPassGeneral(float _valor)
    {
        lowpassGeneral = _valor;
        SetFloat(master, KEY_LOWPASSGENERAL, 21990 - (21990 * _valor) + 10);
        guardat.SetLocal(KEY_LOWPASSGENERAL, lowpassGeneral);
    }

    public void SetLowPassMusica(float _valor)
    {
        lowpassMusica = _valor;
        SetFloat(musica, KEY_LOWPASSMUSICA, 21990 - (21990 * _valor) + 10);
        guardat.SetLocal(KEY_LOWPASSMUSICA, lowpassMusica);
    }

    void SetFloat(AudioMixerGroup _mixer, string _key, float _valor) => _mixer.audioMixer.SetFloat(_key, _valor);



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
        guardat = XS_Editor.LoadGuardat<Guardat>();
        Debugar.Log("[Mixer] OnValidate()");
        Actualitzar();
        if (Application.isPlaying)
        {
            
        }
    }

    public void Actualitzar()
    {
        SetMaster(volumMaster);
        SetMusica(volumMusica);
        SetSo(volumSons);

        if (GetReverberacio() != reverberacio) SetReverberacio(reverberacio);
        if (GetEco() != eco) SetEco(eco);
        if (GetLowPass(master, KEY_LOWPASSGENERAL) != lowpassGeneral) SetLowPassGeneral(lowpassGeneral);
        if (GetLowPass(musica, KEY_LOWPASSMUSICA) != lowpassMusica) SetLowPassMusica(lowpassMusica);
    }
}
