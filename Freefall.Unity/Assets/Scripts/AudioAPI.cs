using UnityEngine;
using System.Collections;

public class AudioAPI : MonoBehaviour  {
    
    public static AudioAPI Instance { get; private set; }

    Hashtable audioHashtable = new Hashtable();
    AudioSource audioSource;

    //Initialize the Audio Source
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        Instance.audioSource = gameObject.AddComponent<AudioSource>();
    }

    public static void PlayTheme(string audioName)
    {
        string audioIntroName = audioName + "_intro";
        string audioLoopName = audioName + "_loop";

        Instance.StartCoroutine(Instance.PlayConnectedTheme(audioIntroName, audioLoopName));
    }

    public static float PlaySong(string audioName, bool looping)
    {
        AudioClip audioClip;

        if (!Instance.audioHashtable.ContainsKey(audioName))
            Instance.LoadAudio(audioName);

        Instance.SetAudioLooping(looping);

        audioClip = (AudioClip)Instance.audioHashtable[audioName];
        Instance.audioSource.clip = audioClip;
        Instance.audioSource.Play();

        return audioClip.length;
    }

    public static void StopSound()
    {
        if (Instance.audioSource.isPlaying)
            Instance.audioSource.Stop();
    }

    public static void PauseSound(AudioSource audio)
    {
        if (Instance.audioSource.isPlaying)
            Instance.audioSource.Pause();
    }

    public static void ResumeSound(AudioSource audio)
    {
        if (!Instance.audioSource.isPlaying && Instance.audioSource.clip != null)
            Instance.audioSource.Play();
    }

    public static void PlaySFX(string audioName)
    {
        AudioClip audioClip;

        if (!Instance.audioHashtable.ContainsKey(audioName))
            Instance.LoadAudio(audioName);

        audioClip = (AudioClip)Instance.audioHashtable[audioName];
        Instance.audioSource.PlayOneShot(audioClip);
    }

    private void LoadAudio(string audioName)
    {
        AudioClip audioClip = (AudioClip)Resources.Load("AudioClips/" + audioName);
        audioHashtable.Add(audioName, audioClip);
    }

    private IEnumerator PlayConnectedTheme(string audioIntroName, string audioLoopName)
    {
        float introSeconds = PlaySong(audioIntroName, false);
        yield return new WaitForSeconds(introSeconds);
        PlaySong(audioLoopName, true); 
    }

    private void SetAudioLooping(bool looping)
    {
        audioSource.loop = looping;
    }
}
