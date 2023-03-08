using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Audio/Mixers", fileName = "Mixers")]
public class Mixers : ScriptableObject 
{
    const string KEY_MASTER = "VolumMaster";
    const string KEY_MUSICA = "VolumMusica";
    const string KEY_SO = "VolumSons";
    const string KEY_REVERB = "Reverberacio";
    const string KEY_ECO = "Eco";
    const string KEY_LOWPASSGENERAL = "LowPassGeneral";
    const string KEY_LOWPASSMUSICA = "LowPassMusica";

    public static Mixers Instance;

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioMixerGroup master;
    [SerializeField] AudioMixerGroup musica;
    [SerializeField] AudioMixerGroup sons;

    [SerializeField] SavableVariable<float> volumMaster;
    [SerializeField] SavableVariable<float> volumMusica;
    [SerializeField] SavableVariable<float> volumSo;
    [Range(0, 1)] [SerializeField] float reverberacio = 0;
    [Range(0, 1)] [SerializeField] float eco = 0;
    [Range(0, 1)] [SerializeField] float lowpassGeneral = 0;
    [Range(0, 1)] [SerializeField] float lowpassMusica = 0;

    public AudioMixerGroup Sons => sons;


    private void OnEnable()
    {
        Debugar.Log("[Mixers] OnEnable() => Instance, Actualitzar()");
        Instance = this;
        Actualitzar();
    }

   

    //SETTERS
    public void SetMaster(float _volum)
    {
        volumMaster.Valor = _volum;
        SetMaster();
    }
    public void SetMusica(float _volum)
    {
        volumMusica.Valor = _volum;
        SetMusica();
    }
    public void SetSo(float _volum)
    {
        volumMaster.Valor = _volum;
        SetSo();
    }
    public void SetReverberacio(float _valor)
    {
        reverberacio = _valor;
        SetReverberacio();
    }
    public void SetEco(float _valor)
    {
        eco = _valor;
        SetEco();
    }
    public void SetLowPassGeneral(float _valor)
    {
        lowpassGeneral = _valor;
        SetLowPassGeneral();
    }
    public void SetLowPassMusica(float _valor)
    {
        lowpassMusica = _valor;
        SetLowPassMusica();
    }




    void SetMaster() => SetFloat(master, KEY_MASTER, ToVolum(volumMaster.Valor));
    void SetMusica() => SetFloat(master, KEY_MUSICA, ToVolum(volumMaster.Valor));
    void SetSo() => SetFloat(sons, KEY_SO, ToVolum(volumSo.Valor));
    void SetReverberacio() => SetFloat(master, KEY_REVERB, ToReverberacio(reverberacio));
    void SetEco() => SetFloat(sons, KEY_ECO, ToEco(eco));
    void SetLowPassGeneral() => SetFloat(master, KEY_LOWPASSGENERAL, ToLowPass(lowpassGeneral));
    void SetLowPassMusica() => SetFloat(musica, KEY_LOWPASSMUSICA, ToLowPass(lowpassMusica));
    void SetFloat(AudioMixerGroup _mixer, string _key, float _valor) => _mixer.audioMixer.SetFloat(_key, _valor);





    public void Actualitzar()
    {
        SetMaster();
        SetMusica();
        SetSo();

        SetReverberacio();
        SetEco();
        SetLowPassGeneral();
        SetLowPassMusica();
    }




    float ToVolum(float volum) => -((volum - 1) * -80);
    float ToReverberacio(float volum) => -10000 + (10000 * volum);
    float ToEco(float volum) => volum / 20f;
    float ToLowPass(float volum) => 21990 - (21990 * volum) + 10;






    private void OnValidate()
    {
        //guardat = XS_Editor.LoadGuardat<Guardat>();
        Debugar.Log("[Mixer] OnValidate()");

        volumMaster = new SavableVariable<float>(KEY_MASTER, Guardat.Direccio.Local, 1);
        volumMusica = new SavableVariable<float>(KEY_MUSICA, Guardat.Direccio.Local, 1);
        volumSo = new SavableVariable<float>(KEY_SO, Guardat.Direccio.Local, 1);

        Actualitzar();
    }
}
