﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorSprite : MonoBehaviour {

    public static Color current_color;

    public Color c;
	// Use this for initialization
	void Start () {
        GameObject btn = GameObject.Find("Red");
        var color = btn.GetComponent<Image>().color;
        ColorSprite.current_color = color;
    }
	

    public void ColorTheSprite() {
        Debug.Log("Coloring the sprite now..");
        GetComponent<SpriteRenderer>().color = current_color;
    }
    
    void OnMouseDown()
    {
        if(GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().color = ColorSprite.current_color;
    }
}
