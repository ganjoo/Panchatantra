using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for " + name);
#pragma warning disable CS0618 // Type or member is obsolete
        Application.LoadLevel(name);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}