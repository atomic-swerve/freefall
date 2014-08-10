using UnityEngine;
using System.Collections;

public class AudioAPI : MonoBehaviour {

    Hashtable audioHashtable = new Hashtable();
    AudioSource audioIntro;
    AudioSource audioLoop;

	// Use this for initialization
	void Start () {
        //PlayTheme("freefall_home_island");
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void PlayTheme(string audioName)
    {
        string audioIntroName = audioName + "_intro";
        string audioLoopName = audioName + "_loop";

        audioIntro = gameObject.AddComponent<AudioSource>();
        audioLoop = gameObject.AddComponent<AudioSource>();

        float introSecs = PlayIntroSong(audioIntroName);
        PlayLoopSong(audioLoopName, introSecs);
    }

    public void StopSound()
    {
        if (audioLoop.isPlaying)
            audioLoop.Stop();
    }

    public void PauseSound(AudioSource audio)
    {
        if (audioLoop.isPlaying)
            audioLoop.Pause();
    }

    public void ResumeSound(AudioSource audio)
    {
        if (!audioLoop.isPlaying && audioLoop.clip != null)
            audioLoop.Play();
    }

    public void PlaySFX(string audioName)
    {
        AudioClip audioClip;

        if (!audioHashtable.ContainsKey(audioName))
            LoadAudio(audioName);;

        audioClip = (AudioClip)audioHashtable[audioName];
        audioLoop.PlayOneShot(audioClip);
    }

    private void LoadAudio(string audioName)
    {
        AudioClip audioClip = (AudioClip)Resources.Load("AudioClips/" + audioName);
        audioHashtable.Add(audioName, audioClip);
    }

    private float PlayIntroSong(string audioName)
    {
        AudioClip audioClip;

        if (!audioHashtable.ContainsKey(audioName))
            LoadAudio(audioName);

        audioClip = (AudioClip)audioHashtable[audioName];
        audioIntro.clip = audioClip;
        audioIntro.Play();

        return audioClip.length;
    }

    private void PlayLoopSong(string audioName, float delaySecs)
    {
        AudioClip audioClip;

        if (!audioHashtable.ContainsKey(audioName))
            LoadAudio(audioName);

        audioLoop.loop = true;

        audioClip = (AudioClip)audioHashtable[audioName];
        audioLoop.clip = audioClip;
        audioLoop.PlayDelayed(delaySecs);
    }
}
