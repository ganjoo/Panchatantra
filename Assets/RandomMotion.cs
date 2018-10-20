using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMotion : MonoBehaviour {

    public float speed = 0.5f;
    public GameObject sprite_prefab;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = new Vector3((float)(Random.value - 0.5) * speed, 0,
                      (float)(Random.value - 0.5) * speed);
        transform.position = Vector3.Lerp(transform.position,
                      transform.position + v, Time.time);
        
    }

    private void OnMouseDown()
    {
        GameObject rocketInstance = Instantiate(sprite_prefab, transform.position, transform.rotation) as GameObject;
    }
}
