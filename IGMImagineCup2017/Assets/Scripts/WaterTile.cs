using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour {

    public GameObject enemy;
    public Vector2 gridPos;

    private float spawnTimer;
    private float timerReset;

    private int difficulty;

	// Use this for initialization
	void Start () {
        Random.InitState((int)System.DateTime.Now.Ticks);

        difficulty = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().difficulty;


        SpawnShips(Random.Range(1, 5 + (int)(difficulty * .2f)));

        spawnTimer = Random.Range(3, 15);
        timerReset = spawnTimer;
    }
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0)
        {
            SpawnShips(Random.Range(1, 5 + (int)(difficulty * .2f)));
            spawnTimer = timerReset;
        }
	}

    void SpawnShips(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float tileSize = GetComponent<Renderer>().bounds.extents.x;
            GameObject newShip = Instantiate(enemy, transform.position, Quaternion.identity);
            newShip.transform.position += new Vector3(Random.Range(-tileSize, tileSize), 1.35f, Random.Range(-tileSize, tileSize));
        }
    }
}
