using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnColorSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void OnClicked(Button button)
    {
        GameObject btn = GameObject.Find(button.name);
        var color = btn.GetComponent<Image>().color;
        ColorSprite.current_color = color;
        print(button.name);
        Debug.Log("Object clicked " + ColorSprite.current_color);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
