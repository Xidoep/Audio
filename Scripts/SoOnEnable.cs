using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoOnEnable : MonoBehaviour
{
    [SerializeField] So so;
    AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = so.Play_Referencia(transform);
    }

    private void OnDisable()
    {
        if (!audioSource)
            return;

        so.Stop(audioSource);
    }
}
