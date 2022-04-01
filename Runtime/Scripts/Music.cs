using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Xido Studio/Audio/Cançó", fileName = "Cançó")]
public class Music : ScriptableObject
{
    [SerializeField] AudioClip audioClip;
    [SerializeField][Range(0,1)] float maximVolume = 1;
    [Tooltip("Set this to FALSE if you want this to be able to replace the main music. Set to TRUE if it's made to be played temporally.")][SerializeField] bool additive = false;
    [Tooltip("If song is Additive, it'll says how much will decrese the main channel.")][SerializeField][Range(0,1)] float overlappingPower = 1;

    public AudioClip AudioClip { get => audioClip; set => audioClip = value; }
    public float MaximVolume => maximVolume;
    public bool Additive => additive;
    public float OverlappingPower => overlappingPower;
}
