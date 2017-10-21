using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour {

    public bool canPort;
    public bool ported;

    private Terrain terrain;

	// Use this for initialization
	void Start () {
        canPort = true;
        ported = false;

        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
	}
	
	// Update is called once per frame
	void Update () {
        TestPort();

        Debug.Log(ported);
	}

    public void TestPort()
    {
    }
}
