using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    //The speed and max speed of the boat
    public float speed;
    public float maxSpeed;

    //The roation speed for the boat, and the current roation amount
    public float rotationSpeed;
    private float rotation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //Accelerate the boat when pressing forward, else decellerate the boat
        if (Input.GetKey(KeyCode.W))
        {
            Accelerate();
        }
        else
        {
            Decellerate();
        }

        //Rotate the boat based on the key input
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(1);
        }
    }

    private void Rotate(int rotateDirection)
    {
        //Calculate the new rotation amount
        //Display the new rotation amount
        rotation += rotationSpeed * rotateDirection;
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
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
