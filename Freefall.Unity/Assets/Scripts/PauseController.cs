using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
    public bool isPaused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Start")) 
        {
            isPaused = !isPaused;
            pauseGame(isPaused);
        }
	}

    // Pause or unpause the game depending on the pause state
    void pauseGame(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            //print("Game is paused.");
        }
        else
        {
            Time.timeScale = 1;
            //print("Game is unpaused");
        }
    }
}
