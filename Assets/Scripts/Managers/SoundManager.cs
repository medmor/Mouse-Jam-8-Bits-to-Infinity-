using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{
    public AudioSource MusicAudioSource;
    public AudioSource EffectsAudioSource;
    public List<AudioClip> SoundsEffects;
    public List<AudioClip> Musics;

    void Start()
    {
        MusicAudioSource.volume = .5f;
        EffectsAudioSource.volume = 1;
    }
    public void PlayEffects(string sound)
    {
        var clip = SoundsEffects.Find(s => s.name == sound);
        EffectsAudioSource.PlayOneShot(clip);
    }
    public void PlayMusic(int clip)
    {
        MusicAudioSource.clip = Musics[clip];
        MusicAudioSource.loop = true;
        MusicAudioSource.Play();
    }
    public void StopMusicAudioSource()
    {
        if (MusicAudioSource.isPlaying)
            MusicAudioSource.Stop();
    }
    public void StopEffectsAudioSource()
    {
        if (EffectsAudioSource.isPlaying)
            EffectsAudioSource.Stop();
    }
    public void TogglePauseMusic()
    {
        if (MusicAudioSource.isPlaying)
        {
            MusicAudioSource.Pause();
        }
        else
        {
            MusicAudioSource.UnPause();
        }
    }
    public void ToggleMusicsMute()
    {
        MusicAudioSource.mute = !MusicAudioSource.mute;
    }
    public void ToggleEffectsMute()
    {
        EffectsAudioSource.mute = !EffectsAudioSource.mute;
    }
}
