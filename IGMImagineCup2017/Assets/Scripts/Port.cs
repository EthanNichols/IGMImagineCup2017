using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour {

    public bool canPort;
    public bool ported;

    private Terrain terrain;

	// Use this for initialization
	void Start () {
        ported = true;
        canPort = false;

        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
	}
	
	// Update is called once per frame
	void Update () {
        TestPort();

        Debug.Log(ported);
	}

    public void TestPort()
    {
        float islandHeight = 1 - terrain.gameObject.GetComponent<PerlinNoise>().heightForIsland;
        if (terrain.SampleHeight(transform.position) > 1 - (islandHeight * 1.5))
        {
            ported = true;
            canPort = false;
        } else
        {
            canPort = true;
        }
    }
}
