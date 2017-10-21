using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //The speed of the bullet
    public int speed;

    //The target the bullet was aimed at, and whether it will hit the target or not
    public GameObject target;
    public bool hitTarget;

    //The initial direction of the bullet
    public Vector3 direction;

    public Vector3 startingVelocity;

	// Use this for initialization
	void Start () {
        //Move the bullet above the ship
        transform.position += new Vector3(0, .5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        //Move the bullet
        Move();

        //If the bullet goes below the water destroy the bullet
        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        //If the bullet will hit the target move the bullet towards the target
        if (hitTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        //If the bullet won't hit the target, just move the bullet in the right direction
        else
        {
            transform.position += (startingVelocity + direction.normalized).normalized * speed * Time.deltaTime;
            transform.position -= new Vector3(0, .01f, 0);
        }
    }
}
