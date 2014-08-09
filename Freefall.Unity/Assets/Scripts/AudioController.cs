using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

    public void PlaySound(AudioSource audio)
    {
        audio.Play();
    }

    public void StopSound(AudioSource audio)
    {
        if (audio.isPlaying)
            audio.Stop();
    }

    public void PauseSound(AudioSource audio)
    {
        audio.Pause();
    }

    public void setSoundLoop(AudioSource audio)
    {
        audio.loop = true;
    }

    public void PlayDelayedSound(AudioSource audio, float delaySeconds)
    {
        audio.PlayDelayed(delaySeconds);
    }
}
