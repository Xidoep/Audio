using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Utils_Volume_Music : Utils_Volume
{
    [SerializeField] MusicControlador musicControlador;
    [SerializeField] Music music;

    public override void OnAproaching(float distance) 
    {
        musicControlador.Play(music, distance);
    } 
    public override void OnFullyEntering() { }
    public override void OnExit() { }


    private void OnValidate()
    {
        musicControlador = XS_Utils.XS_Editor.LoadAssetAtPath<MusicControlador>("Assets/XidoStudio/Audio/Runtime/Musica.asset");
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        switch (MyTypes)
        {
            case Types.spfere:
                Gizmos.DrawWireSphere(transform.position, ShpereSize + Range);
                Gizmos.DrawSphere(transform.position, ShpereSize);
                break;
            default:
                Gizmos.DrawWireCube(transform.position, BoxSize + (Vector3.one * (Range * 2)));
                Gizmos.DrawCube(transform.position, BoxSize);
                break;
        }

    }

}
