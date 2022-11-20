using UnityEngine;

public enum BGMType
{
    None = -1,
    InGame,
}

public enum SFXType
{
    None = -1,
    Page,
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private BGMType currentBGMClip = BGMType.None;
    public float BGMVolume
    {
        get => bgmSource.volume;
        set => bgmSource.volume = value;
    }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] sfxClips;
    public float SFXVolume
    {
        get => sfxSource.volume;
        set => sfxSource.volume = value;
    }

    public void PlayBGM(BGMType type)
    {
        if (type == BGMType.None) return; 
        
        bgmSource.clip = bgmClips[(int) type];
        bgmSource.Play();

        currentBGMClip = type;
    }

    public void StopBGM()
    {
        bgmSource.Stop();

        currentBGMClip = BGMType.None;
    }

    public void PlaySFX(SFXType type)
    {
        if (type == SFXType.None) return; 

        sfxSource.PlayOneShot(sfxClips[(int) type]);
    }
}