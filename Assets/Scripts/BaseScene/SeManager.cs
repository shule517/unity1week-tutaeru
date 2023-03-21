using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SeManager : SingletonMonoBehaviour<SeManager>
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioSource audioSource2;
    [SerializeField] private AudioClip[] audioClips;
    private Dictionary<string, AudioClip> audioClipDict;

    void Start()
    {
        // 次のシーンでも破棄しない
        DontDestroyOnLoad(gameObject);

        audioClips = Resources.LoadAll<AudioClip>("SE");
        audioClipDict = audioClips.ToDictionary(clip => clip.name, clip => clip);
    }

    public void Play(string filePath, float pitch = 1f, float volumeScale = 1f, int audioSourceNo = 1)
    {
        var audioClip = audioClipDict[filePath];
        var audio = audioSourceNo == 1 ? audioSource : audioSource2;

        audio.pitch = pitch;
        audio.PlayOneShot(audioClip, volumeScale);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
