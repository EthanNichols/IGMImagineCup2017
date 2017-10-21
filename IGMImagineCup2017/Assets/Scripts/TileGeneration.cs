using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{

    public Vector2 tilePos = Vector2.zero;
    public GameObject waterTile;

    private List<GameObject> waterTiles = new List<GameObject>();
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        bool newTiles = MoveGrid();

        if (newTiles)
        {
            DeleteTiles();
            CreateTiles();
        }
    }

    private bool MoveGrid()
    {
        Vector3 tileSize = waterTile.GetComponent<Renderer>().bounds.extents;
        Vector2 oldPos = tilePos;

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

        if (tilePos != oldPos)
        {
            return true;
        }

        return false;
    }

    private void CreateTiles()
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                bool createTile = true;

                foreach (GameObject tile in waterTiles)
                {
                    if (tile.GetComponent<WaterTile>().gridPos == new Vector2(tilePos.x + x, tilePos.y + x))
                    {
                        createTile = false;
                    }
                }

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
        for (int i = 0; i < waterTiles.Count; i++)
        {
            if ((waterTiles[i].GetComponent<WaterTile>().gridPos.x + 1 == tilePos.x ||
                waterTiles[i].GetComponent<WaterTile>().gridPos.x - 1 == tilePos.x) &&
                (waterTiles[i].GetComponent<WaterTile>().gridPos.y + 1 == tilePos.y ||
                waterTiles[i].GetComponent<WaterTile>().gridPos.y - 1 == tilePos.y) ||
                waterTiles[i].GetComponent<WaterTile>().gridPos == tilePos)
            {
                continue;
            }

            GameObject tile = waterTiles[i];
            waterTiles.Remove(tile);
            Destroy(tile);
        }
    }
}
