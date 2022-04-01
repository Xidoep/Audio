using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public class SoProva : MonoBehaviour
{
    public So so1;
    public So so2;
    public So so3;

    SoControlador audioSource;
    [SerializeField] MusicControlador musica;

    public Music musica1;
    [Range(0,1)]public float volume1;
    [Space(20)]
    public Music musica2;
    [Range(0, 1)] public float volume2;


    [ContextMenu("Prova1")]
    public void Prova1()
    {
        audioSource = so1.Play(transform);
    }

    [ContextMenu("Apagar2")]
    public void Apagar2()
    {
        if (audioSource == null)
            return;

        audioSource.Apagar();
        audioSource = null;
    }

    //FALTA






    private void Update()
    {
        if (Key.Digit1.OnPress()) musica.Play(musica1, volume1);
        if (Key.Digit2.OnPress()) musica.Play(musica2, volume2);

    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            return;

        //if (volume1 > 0) musica.Play(musica1, volume1);
        //if (volume2 > 0) musica.Play(musica2, volume2);
    }

}
