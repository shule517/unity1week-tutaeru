using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BgmManager : SingletonMonoBehaviour<BgmManager>
{
    public AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    private Dictionary<string, AudioClip> audioClipDict;

    void Start()
    {
        // 次のシーンでも破棄しない
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioClips = Resources.LoadAll<AudioClip>("BGM");
        audioClipDict = audioClips.ToDictionary(clip => clip.name, clip => clip);
    }

    public void Play(string filePath)
    {
        audioSource.Stop();
        audioSource.volume = 1f;

        var audioClip = audioClipDict[filePath];
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
