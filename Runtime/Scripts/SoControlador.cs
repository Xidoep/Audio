using UnityEngine;
using XS_Utils;

public class SoControlador : MonoBehaviour
{
    AudioSource audioSource;
    //public AudioSource AudioSource => audioSource;
    XS_Countdown compteEnrere;

    System.Action<SoControlador> enRelease;

    public bool IsPlaying => audioSource.isPlaying;

    public SoControlador Crear(AudioSource audioSource)
    {
        //Debugar.Log("Crear");
        this.audioSource = audioSource;
        this.audioSource.playOnAwake = false;
        compteEnrere = new XS_Countdown(3, Release, true);
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
    public void Play(float delay)
    {
        Debug.Log($"Delay = {delay} | play on awake = {audioSource.playOnAwake}");
        audioSource.PlayDelayed(delay);
        //audioSource.PlayScheduled(delay);
        //XS_Coroutine.StartCoroutine_Ending_FrameDependant(delay, () => audioSource.Play());
    }
    public void Stop() => audioSource.Stop();
    public AudioClip Clip {set => audioSource.clip = value; }
    public float Volume {set => audioSource.volume = value; }
    public float Pitch {set => audioSource.pitch = value; }
    public bool Loop { set => audioSource.loop = value; }
    public void AudioMixed()
    {
        if (audioSource.outputAudioMixerGroup == null) audioSource.outputAudioMixerGroup = Mixers.Instance.Sons;
    }
    public float SpatialBlend { set => audioSource.spatialBlend = value; }
    public float MaxDistance { set => audioSource.maxDistance = value; }


} 
