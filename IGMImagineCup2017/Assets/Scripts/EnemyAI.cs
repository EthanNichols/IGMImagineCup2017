using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Explosion;

    //The speed and max speed of the boat
    public float speed;
    public float maxSpeed;

    //The distance enemies can see the player from
    //The distance the enemy can get to the player
    public float viewDistance;
    public float proximityDistance;

    //The roation speed for the boat, and the current roation amount
    public float rotationSpeed;
    private float rotation;
    private Vector3 rotateDirection;

    private int difficulty;
    private GameObject canvas;

    private GameObject player;
    private Terrain terrain;

    // Use this for initialization
    void Start()
    {
        //Set the player
        player = GameObject.FindGameObjectWithTag("Player");
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();

        canvas = GameObject.FindGameObjectWithTag("Canvas");
        difficulty = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().difficulty;

        speed += .5f * difficulty;
        rotationSpeed += .2f * difficulty;

        if (Vector3.Distance(transform.position, player.transform.position) < 30)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get the distance from the enemy to the player
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);

        if (terrain.SampleHeight(transform.position) > .4f)
        {
            RandomRotate();
        }
        else
        {
            Rotate();
        }

        //Move the enemy
        Accelerate();

        if (Vector3.Distance(player.transform.position, transform.position) > 300)
        {
            Destroy(gameObject);
        }
    }

    private void Rotate()
    {
        //Get the distance from the enemy to the ship
        Vector3 targetDir = player.transform.position - transform.position;

        //The rotation amount per frame, and increase the amount of rotation done
        float step = rotationSpeed * Time.deltaTime;
        rotation += step;

        //Rotate the enemy towards the ship
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void RandomRotate()
    {
        //The rotation amount per frame, and increase the amount of rotation done
        float step = rotationSpeed * Time.deltaTime;
        rotation += step;

        //Rotate the ship to avoid colliding with the player
        Vector3 newDir = Vector3.RotateTowards(transform.forward, -transform.forward + rotateDirection, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void Accelerate()
    {
        //Apply instant velocity to the ship
        GetComponent<Rigidbody>().velocity = transform.forward * speed;

        //Cap the max speed for the boat
        Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, maxSpeed);
    }

    private void Decellerate()
    {
        //Slow the boat down relative to the velocity
        GetComponent<Rigidbody>().velocity *= .97f;

        //Set the velocity to 0 if it is significantly small
        if (GetComponent<Rigidbody>().velocity.magnitude < .2f) { GetComponent<Rigidbody>().velocity = Vector3.zero; }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            canvas.GetComponent<UIManager>().AddScore(10);
        }

        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<Move>().Ramming)
            {
                Destroy(gameObject);
                canvas.GetComponent<UIManager>().AddScore(20);
            }
        }
    }

    private void OnMouseDown()
    {
        player.GetComponent<CannonBehavior>().setTarget = gameObject;
    }
}
