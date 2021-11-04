using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using XS_Utils;


public static class SonsPoolAutomatic
{
    static bool iniciat = false;
    static ObjectPool<SoControlador> poolAuto;
    public static ObjectPool<SoControlador> Pool => poolAuto;


    static GameObject tmp;
    static SoControlador so;


    public static SoControlador Get(bool loop)
    {
        if (!iniciat)
            Iniciar();
        return Pool.Get().Iniciar(Release, loop);
    }


    static void Iniciar()
    {
        poolAuto = new ObjectPool<SoControlador>(Crear, OnPoolGet, OnPoolRelease);
        iniciat = true;
    }

    static SoControlador Crear()
    {
        tmp = new GameObject("so");
        so = tmp.AddComponent<SoControlador>();
        so.audioSource = tmp.AddComponent<AudioSource>();
        //Debug.Log("Crear");
        return so;
    }
    static void OnPoolGet(SoControlador so)
    {
        so.gameObject.SetActive(true);
        //Debug.Log("Get");
    }
    static void OnPoolRelease(SoControlador so)
    {
        so.gameObject.SetActive(false);
        //Debug.Log("Release");
    }


    static void Release(SoControlador soControlador)
    {
        Pool.Release(soControlador);
    }
}
