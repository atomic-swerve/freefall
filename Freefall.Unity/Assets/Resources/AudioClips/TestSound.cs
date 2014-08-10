using UnityEngine;
using System.Collections;

public class TestSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioAPI audioAPI = this.GetComponent<AudioAPI>();
        audioAPI.PlayTheme("freefall_home_island");
        for (int i = 0; i < 10; i++)
        {
            audioAPI.PlaySFX("freefall_hit");
        }
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
