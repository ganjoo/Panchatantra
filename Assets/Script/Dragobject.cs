﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dragobject : MonoBehaviour {
  private Vector3 screenPoint;
  private Vector3 offset;
  public GameObject target;
  private Vector3 initialPos;


    public delegate void targetFoundCallback();
    public static targetFoundCallback targetFound;

    void OnMouseDown(){

    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }


  void OnMouseUp(){
        if (target == null)
            return;
     float distance = Vector2.Distance(target.transform.position,transform.position);
    Debug.Log(distance);
    if(distance < 2){
      transform.position = target.transform.position;

            //TODO The following delegate requires that targetFound be mapped in some slot(function) in another script in the scene
            // So before using this as a part of the prefab, please add targetFound receiver in some other script e.g. Score script...
            targetFound();
     
        }

    else{
      transform.position = initialPos;
    }
    Debug.Log("Mouse Up");
       

    }

    void OnMouseDrag(){
    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
    transform.position = cursorPosition;
       // Debug.Log("Mouse Dragged");

  }
	// Use this for initialization
	void Start () {
		initialPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
