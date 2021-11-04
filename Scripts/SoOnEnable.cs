using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoOnEnable : MonoBehaviour
{
    [SerializeField] So so;
    SoControlador audioSource;

    private void OnEnable()
    {
        audioSource = so.Play(transform);
    }

    private void OnDisable()
    {
        if (!audioSource)
            return;

        audioSource.Apagar();
    }
}
