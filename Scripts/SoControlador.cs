using UnityEngine;
using XS_Utils;

public class SoControlador : MonoBehaviour
{
    AudioSource audioSource;
    public AudioSource AudioSource => audioSource;
    Countdown compteEnrere;

    System.Action<SoControlador> enRelease;
    public SoControlador Crear(AudioSource audioSource)
    {
        Debug.Log("Crear");
        this.audioSource = audioSource;
        compteEnrere = new Countdown(3, Release);
        return this;
    }
    void Release() => enRelease.Invoke(this);
    public SoControlador Iniciar(System.Action<SoControlador> enRelease, bool loop)
    {
        Debug.Log("Iniciar");
        this.enRelease = enRelease;
        //if(temps > 0)
        //{
        if (compteEnrere == null) compteEnrere.Start();
        if (!loop) compteEnrere.Start();

        //}

        return this;
    }

    void Update()
    {
        compteEnrere.Update();
    }
    public void Apagar()
    {
        Debug.Log("Apagar");
        audioSource.Stop();
        enRelease.Invoke(this);
    }
}
