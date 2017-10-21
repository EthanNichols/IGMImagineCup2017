using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

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

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        //Set the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Get the distance from the enemy to the player
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);

        //If the enemy can see the player
        if (playerDistance < viewDistance)
        {

            //Check if the player is far from the player, rotate to face towards the player
            if (playerDistance > proximityDistance)
            {
                Rotate();

                rotateDirection = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            }

            //If the enemy is too close to the enemy move change its course
            else
            {
                RandomRotate();
            }

            //Move the enemy
            Accelerate();
        }
        else
        {
            //Slow the enemy down
            Decellerate();
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
        Vector3 newDir = Vector3.RotateTowards(transform.forward, transform.position + rotateDirection, step, 0.0F);
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
}
