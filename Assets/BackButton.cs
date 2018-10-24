using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {


	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Level load requested for " + name);
#pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel("Menu");
#pragma warning restore CS0618 // Type or member is obsolete

            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
