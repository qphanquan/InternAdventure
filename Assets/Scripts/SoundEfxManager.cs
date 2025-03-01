using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEfxManager : MonoSingleton<SoundEfxManager>
{
    public AudioSource MusicSource;
    public AudioSource SoundSource;

    protected override void Awake()
    {
        base.Awake();
    }

    private void PlayMusic(AudioClip clip)
    {
        this.MusicSource.clip = clip;
        this.MusicSource.volume = 1;
        this.MusicSource.loop = true;
        this.MusicSource.Play();
    }

    private void PlaySound(AudioClip clip)
    {
        this.SoundSource.clip = clip;
        this.SoundSource.volume = 1;
        this.SoundSource.loop = false;
        this.SoundSource.Play();
    }

    public void PlayMusicBackground()
    {
        AudioClip clip = ResourceManager.Instance.GetAudioClip("Background");
        this.PlayMusic(clip);
    }

    public void PlaySoundJump()
    {
        AudioClip clip = ResourceManager.Instance.GetAudioClip("Jump");
        this.PlaySound(clip);
    }

    public void PlaySoundLose()
    {
        AudioClip clip = ResourceManager.Instance.GetAudioClip("Lose");
        this.PlaySound(clip);
    }

    public void PlaySoundWin()
    {
        AudioClip clip = ResourceManager.Instance.GetAudioClip("Win");
        this.PlaySound(clip);
    }
}
