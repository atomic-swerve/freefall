using UnityEngine;
using System.Collections;

public class AudioAPI : MonoBehaviour {

    Hashtable audioHashtable = new Hashtable();
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayTheme(string audioName)
    {
        string audioIntroName = audioName + "_intro";
        string audioLoopName = audioName + "_loop";

        StartCoroutine(PlayConnectedTheme(audioIntroName, audioLoopName));
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    public void PauseSound(AudioSource audio)
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
    }

    public void ResumeSound(AudioSource audio)
    {
        if (!audioSource.isPlaying && audioSource.clip != null)
            audioSource.Play();
    }

    public void PlaySFX(string audioName)
    {
        AudioClip audioClip;

        if (!audioHashtable.ContainsKey(audioName))
            LoadAudio(audioName);

        audioClip = (AudioClip)audioHashtable[audioName];
        audioSource.PlayOneShot(audioClip);
    }

    private void LoadAudio(string audioName)
    {
        AudioClip audioClip = (AudioClip)Resources.Load("AudioClips/" + audioName);
        audioHashtable.Add(audioName, audioClip);
    }

    private float PlaySong(string audioName, bool looping)
    {
        AudioClip audioClip;

        if (!audioHashtable.ContainsKey(audioName))
            LoadAudio(audioName);

        SetAudioLooping(looping);

        audioClip = (AudioClip)audioHashtable[audioName];
        audioSource.clip = audioClip;
        audioSource.Play();

        return audioClip.length;
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
