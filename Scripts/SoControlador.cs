using UnityEngine;
using XS_Utils;

public class SoControlador : MonoBehaviour
{
    public AudioSource audioSource;

    Countdown compteEnrere;

    System.Action<SoControlador> enRelease;
    public SoControlador Iniciar(System.Action<SoControlador> enRelease, bool loop)
    {
        this.enRelease = enRelease;
        //if(temps > 0)
        //{
        if (compteEnrere == null) compteEnrere = new Countdown(3, () => enRelease.Invoke(this));
        if (!loop) compteEnrere.Start();

        //}

        return this;
    }

    private void Update()
    {
        compteEnrere.Update();
    }
    public void Apagar()
    {
        audioSource.Stop();
        enRelease.Invoke(this);
    }
}
