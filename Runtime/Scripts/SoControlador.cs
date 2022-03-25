using UnityEngine;
using XS_Utils;

public class SoControlador : MonoBehaviour
{
    AudioSource audioSource;
    //public AudioSource AudioSource => audioSource;
    XS_Countdown compteEnrere;

    System.Action<SoControlador> enRelease;
    public SoControlador Crear(AudioSource audioSource)
    {
        Debug.Log("Crear");
        this.audioSource = audioSource;
        compteEnrere = new XS_Countdown(3, Release);
        return this;
    }
    void Release() => enRelease.Invoke(this);
    public SoControlador Iniciar(System.Action<SoControlador> enRelease, bool loop)
    {
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

    public void Play() => audioSource.Play();
    public void Stop() => audioSource.Stop();
    public AudioClip Clip {set => audioSource.clip = value; }
    public float Volume {set => audioSource.volume = value; }
    public float Pitch {set => audioSource.pitch = value; }
    public bool Loop { set => audioSource.loop = value; }
    public void AudioMixed()
    {
        if (audioSource.outputAudioMixerGroup == null) audioSource.outputAudioMixerGroup = Mixers.Instance.Sons;
    }
    public bool SpatialBlend { set => audioSource.spatialBlend = Application.isPlaying ? (value ? 1 : 0) : 0; }
    public float MaxDistance { set => audioSource.maxDistance = value; }

} 
