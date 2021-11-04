using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoProva : MonoBehaviour
{
    public So so1;
    public So so2;
    public So so3;

    SoControlador audioSource;

    public Musica musica1;
    public Musica musica2;
    public Musica musica3;
    [Range(0,1)]public float musica1Volum;
    [Range(0,1)]public float musica2Volum;
    [Range(0,1)]public float musica3Volum;

    private void OnEnable()
    {
        //SonsPoolAutomatic.Iniciar();
    }

    private void OnValidate()
    {
        musica2Volum = 1 - musica1Volum;
        musica1.Volum(musica1Volum);
        if (musica2) musica2.Volum(musica2Volum);
        if (musica3) musica3.Volum(musica3Volum);
        //musica1.Play(musica1Volum);
        //musica2.Play(musica2Volum);
        //musica1.Play(musica1Volum);
        //musica2.Play(musica2Volum);
    }

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

    [ContextMenu("Musica1")]
    public void Musica1()
    {
        musica1.Play();
    }
    [ContextMenu("Musica2")]
    public void Musica2()
    {
        //musica2.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) so1.Play();
        if (Input.GetKeyDown(KeyCode.Alpha2)) so2.Play();
        if (Input.GetKeyDown(KeyCode.Alpha3)) so3.Play();
    }

}
