using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    public GameObject nucleus; //This will be assigned when the object is instansiated
    public GameObject sphere;

    public float speed = 0.05f;
    public Vector3 source;
    public Vector3 noise = new Vector3(0,0,0);
    public Vector3 pos;
    private Transform electron;
    public bool instanciated = false;

    public Renderer rend;
	// Use this for initialization
	void Start () {
        pos = transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
        noise = new Vector3((Random.Range(-0.01f, 0.01f)), (Random.Range(-0.01f, 0.01f)), (Random.Range(-0.01f, 0.01f)));
        if(instanciated)//So we don't appear in the wrong place
        {
            //testing noise jitter
            //transform.position = noise;
            pos.x += speed;
            transform.position = pos;
            
        }
    }

    void wander()
    {

    }
}
