using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    private GameObject terrain;
	// Use this for initialization
	void Start ()
    {
        terrain = GameObject.FindGameObjectWithTag("Terrain");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
