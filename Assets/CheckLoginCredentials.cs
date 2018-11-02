using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckLoginCredentials : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Write fb id, email and name in local db

        PlayerStats.email = PlayerPrefs.GetString("fb_email");
        PlayerStats.name = PlayerPrefs.GetString("fb_name");
        PlayerStats.fb_id = PlayerPrefs.GetString("fb_id");
        if (PlayerStats.email.Length > 0) {
            SceneManager.LoadScene("Menu");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
