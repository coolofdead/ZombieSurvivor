using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource themeAudioSource;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayTheme(IEnumerable<AudioClip> clips)
    {
        themeAudioSource.DOFade(0, 1.25f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            StopAllCoroutines();
            StartCoroutine(PlayPlaylist(clips));

            themeAudioSource.DOFade(1, 0.65f).SetEase(Ease.OutSine);
        });
    }

    public void PlayTheme(AudioClip clip)
    {
        PlayTheme(new List<AudioClip>() { clip });
    }

    IEnumerator PlayPlaylist(IEnumerable<AudioClip> clips)
    {
        while (true)
        {
            foreach (var clip in clips)
            {
                themeAudioSource.clip = clip;
                themeAudioSource.Play();

                yield return new WaitWhile(() => themeAudioSource.isPlaying);
            }
        }
    }
}
