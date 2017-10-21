using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    //The enter tile position for the water tiles
    public Vector2 tilePos = Vector2.zero;

    //The water tile prfab, and the list of water tiles in the scene
    public GameObject waterTile;
    private List<GameObject> waterTiles = new List<GameObject>();

    //The player
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        //Set the player gameobject
        player = GameObject.FindGameObjectWithTag("Player");

        //Create the starting tiles for the game
        CreateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        //Test if the grid moved
        bool newTiles = MoveGrid();

        //If the grid moved create new tiles
        if (newTiles)
        {
            CreateTiles();
        }

        //Constantly delete tiles that aren't needed
        DeleteTiles();
    }

    private bool MoveGrid()
    {
        //The size of the tile object
        //The previous position for the center tile
        Vector3 tileSize = waterTile.GetComponent<Renderer>().bounds.extents;
        Vector2 oldPos = tilePos;

        //Test if the player enters the area of a new tile
        //Change the center location for the tile grid
        if (player.transform.position.x - tileSize.x * tilePos.x >= tileSize.x * tilePos.x + tileSize.x)
        {
            tilePos += new Vector2(1, 0);
        }
        else if (player.transform.position.x - tileSize.x * tilePos.x <= tileSize.x * tilePos.x - tileSize.x)
        {
            tilePos -= new Vector2(1, 0);
        }

        if (player.transform.position.z - tileSize.z * tilePos.y >= tileSize.z * tilePos.y + tileSize.z)
        {
            tilePos += new Vector2(0, 1);
        }
        else if (player.transform.position.z - tileSize.z * tilePos.y <= tileSize.z * tilePos.y - tileSize.z)
        {
            tilePos -= new Vector2(0, 1);
        }

        //If the current and old position don't match return true, else return false
        if (tilePos != oldPos)
        {
            return true;
        }

        return false;
    }

    private void CreateTiles()
    {

        //Loop through all possible X and Y positions for new tiles
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //Whether to create the tile or not
                bool createTile = true;

                //Loop through all of the tiles in the scene
                foreach (GameObject tile in waterTiles)
                {
                    //If the grid positions match don't create a new tile
                    if (tile.GetComponent<WaterTile>().gridPos == new Vector2(tilePos.x + x, tilePos.y + y))
                    {
                        createTile = false;
                        break;
                    }
                }

                //Create a new tile for the map
                if (createTile)
                {
                    GameObject newTile = Instantiate(waterTile, Vector3.zero, Quaternion.identity);
                    newTile.transform.position = new Vector3(tilePos.x + x, 0, tilePos.y + y) * newTile.GetComponent<Renderer>().bounds.size.x;
                    waterTiles.Add(newTile);
                    newTile.GetComponent<WaterTile>().gridPos = new Vector2(tilePos.x + x, tilePos.y + y);
                }
            }
        }
    }

    private void DeleteTiles()
    {
        //Loop through all of the tiles in the scene
        for (int i = 0; i < waterTiles.Count; i++)
        {
            //Test if the tile is far away from the center tile
            //Delete the tile and remove it from the list of tiles in the scene
            if (Vector2.Distance(tilePos, waterTiles[i].GetComponent<WaterTile>().gridPos) >= 1.5f)
            {
                GameObject tile = waterTiles[i];
                waterTiles.Remove(tile);
                Destroy(tile);
            }
        }
    }
}
