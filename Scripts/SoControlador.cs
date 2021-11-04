using UnityEngine;
using XS_Utils;

public class SoControlador : MonoBehaviour
{
    public AudioSource audioSource;

    CompteEnrere compteEnrere;

    System.Action<SoControlador> enRelease;
    public SoControlador Iniciar(System.Action<SoControlador> enRelease, bool loop)
    {
        this.enRelease = enRelease;
        //if(temps > 0)
        //{
        if (compteEnrere == null) compteEnrere = new CompteEnrere(3, () => enRelease.Invoke(this));
        if (!loop) compteEnrere.Iniciar();

        //}

        return this;
    }

    private void Update()
    {
        compteEnrere.Actualitzar();
    }
    public void Apagar()
    {
        audioSource.Stop();
        enRelease.Invoke(this);
    }
}
