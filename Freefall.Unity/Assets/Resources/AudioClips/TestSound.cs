using UnityEngine;
using System.Collections;

public class TestSound : MonoBehaviour {

    AudioAPI audioAPI;

	// Use this for initialization
	void Start () {
        audioAPI = this.GetComponent<AudioAPI>();
        audioAPI.PlayTheme("freefall_home_island"); 
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("B"))
        {
            audioAPI.PlaySFX("freefall_hit");
        }
	}
}
