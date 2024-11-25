using UnityEngine;

public class AudioSourceWrapper : IAudioSource
{
    private readonly AudioSource _audioSource;

    public AudioSourceWrapper(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }

    public void PlayOneShot(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
