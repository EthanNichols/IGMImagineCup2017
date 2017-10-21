using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour {

    //The seed for the game
    public int seed = 0;

    //The terrain, terrain resolution, terrain width/height/length
    private Terrain terrain;
    public int terrainResolution = 256;
    public Vector3 terrainSize = new Vector3(200, 30, 200);

    //The step size for the perlin noise
    public float stepSize;
    public float heightForIsland = .9f;

    //The current position, and the previous position
    public Vector2 position;
    private Vector2 oldPosition;

	// Use this for initialization
	void Start () {

        //Set the information about the terrain
        terrain = GetComponent<Terrain>();
        terrain.terrainData.size = terrainSize;
        terrain.terrainData.heightmapResolution = terrainResolution;

        //Generate a seed if the seed is 0
        if (seed == 0)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
        } else
        {
            Random.InitState(seed);
        }

        //Get a random starting position for the map position
        position = StartingPosition();

        //Calculate the perlin noise for the terrain
        CalculatePerlin();
	}
	
	// Update is called once per frame
	void Update () {

        //If the player  moved calculate the new terrain to display
        if (position != oldPosition)
        {
            CalculatePerlin();
        }

        //Set the previous position to the current position
        oldPosition = position;
    }

    private Vector2 StartingPosition()
    {
        //Set a random starting position for the player
        return new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
    }

    private void CalculatePerlin()
    {
        //Array of heights for the terrain
        float[,] heights = new float[terrainResolution, terrainResolution];

        //Loop through all of the positions on the terrain
        for (int x = 0; x < terrain.terrainData.heightmapResolution - 1; x++)
        {
            for (int y = 0; y < terrain.terrainData.heightmapResolution - 1; y++)
            {
                //Set the height of the terrain
                heights[x, y] = Mathf.PerlinNoise((position.x + x) * stepSize, (position.y + y) * stepSize);

                if (heights[x, y] <= heightForIsland)
                {
                    heights[x, y] -= (1 - heightForIsland);
                }
            }
        }
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
