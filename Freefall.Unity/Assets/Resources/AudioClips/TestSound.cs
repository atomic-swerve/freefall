using UnityEngine;
using System.Collections;

public class TestSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioAPI audioAPI = this.GetComponent<AudioAPI>();
        audioAPI.PlayTheme("freefall_home_island");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
