using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Audio/MusicaControlador", fileName = "Musica")]
public class MusicControlador : ScriptableObject
{
    [SerializeField] AudioMixerGroup musicaMixer;

    [SerializeField] Channel main;
    [SerializeField] Channel additive;


    private void OnEnable()
    {
        main.OnEnable(musicaMixer);
        additive.OnEnable(musicaMixer);
    }
    private void OnDisable()
    {
        main.OnEnable(musicaMixer);
        additive.OnEnable(musicaMixer);
    }

    public void Play(Music music, float volume)
    {
        //If there is no song or the main is the same I trying to play. Creates one or ajut its volume.
        if(main.Music == null || main.Music == music)
        {
            main.Set(music, volume);

            //In the case that there are another song playing, ajust the two volumens.
            if (additive.Music != null)
            {
                additive.Set(1 - volume);
                if (additive.VolumeZero) additive.Stop();
            }

            if (main.VolumeZero) 
            {
                main.Stop();

                //In the case that the secundary music is mean to replace the mina. Do it.
                if(additive.Music != null && !additive.Music.Additive)
                {
                    main.Substituir(additive);
                }
            } 
        }
        else //If the song I try to play is not the ones that i playing, it creates another one, or ajust this second volume.
        {
            if(additive.Music == null || additive.Music == music)
            {
                //As I know there are a main song, ajust the two volumens.
                additive.Set(music, volume);
                main.Set(1 - (volume * (additive.Music.Additive ? additive.Music.OverlappingPower : 1)));

                //in the case that the second is meant to replace the first and the first reach zero volume, put the second one as main.
                if (main.VolumeZero && !additive.Music.Additive)
                {
                    main.Stop();
                    main.Substituir(additive);
                    return;
                }

                if (additive.VolumeZero) additive.Stop();
            }
            else
            {
                Debugar.LogError("[Musica] - Play(); ***Seams that you are trying to play 3 songs at the same time. Maybe it's too much...***");
            }
        }

    }
    public void PlayExtra(Music music, float volume)
    {
        additive.Set(music, volume);
    }

    [System.Serializable]
    public class Channel
    {
        AudioMixerGroup audioMixerGroup;
        AudioSource audioSource;
        [SerializeField] Music music;
        [Range(0, 1)] [SerializeField] float volume;

        public Music Music => music;
        public bool VolumeZero => volume == 0;
        


        public void OnEnable(AudioMixerGroup audioMixerGroup)
        {
            this.audioMixerGroup = audioMixerGroup;
            Clean();
        }
        public void Substituir(Channel channel)
        {
            audioSource = channel.audioSource;
            music = channel.music;
            volume = channel.volume;
            channel.Clean();
        }

        void Init(Music music)
        {
            if (audioSource != null)
                return;

            this.music = music;

            GameObject tmp = new GameObject("Music");
            audioSource = tmp.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.playOnAwake = false;
            audioSource.loop = true;
            audioSource.spatialBlend = 0;
            audioSource.clip = music.AudioClip;

            SetVolume(0);

            if (!audioSource.isPlaying) audioSource.Play();
        }

        public void Set(Music music, float volume)
        {
            Init(music);
            SetVolume(volume);
        }
        public void Set(float volume)
        {
            
            SetVolume(volume);
        }

        void SetVolume(float volume)
        {
            this.volume = volume;
            audioSource.volume = Mathf.Lerp(0, music.MaximVolume, volume);
        }

        public void Stop()
        {
            audioSource.Stop();
            Destroy(audioSource.gameObject);
            Clean();
        }
        void Clean()
        {
            audioSource = null;
            music = null;
            volume = 0;
        }

        /*
        float startingVolume = 0;
        float endingVolume = 1;
        float startingTime = 0;
        float endingTime = 1;
        float progress = 0;

        public void Set(AudioClip clip, float volume, float time)
        {
            Set(clip);
            MainStartSetVolume(volume, time);
        }
        public void Set(float volume)
        {
            Init();
            Volume = volume;
        }
        public void Set(float volume, float time)
        {
            Init();
            MainStartSetVolume(volume, time);
        }*/






        /*void MainStartSetVolume(float volume, float time)
        {
            startingVolume = audioSource.volume;
            endingVolume = volume;
            startingTime = Time.time;
            endingTime = Time.time + time;
            progress = 0;
            XS_Coroutine.StartCoroutine_Update(IsVolumeReached, SetVolumeByTime);
        }

        void SetVolumeByTime()
        {
            progress = Mathf.InverseLerp(startingTime, endingTime, Time.time);
            Volume = Mathf.Lerp(startingVolume, endingVolume, progress);
        }

        bool IsVolumeReached() => progress == 1;
        */
    }
}
