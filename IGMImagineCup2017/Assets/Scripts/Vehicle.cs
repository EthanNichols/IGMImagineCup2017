using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Chase Slattery
//Class that handles ship movement, bullet creation, and acceleration
public class Vehicle : MonoBehaviour 
{
	public Vector3 position;
	//public float speed;				// NO LONGER NECESSARY SINCE WE HAVE AN ACCELERATION RATE
	public Vector3 direction;
	public Vector3 velocity;

	// For acceleration
	public Vector3 acceleration;	// will be calculated each frame
	public float maxSpeed;			// must be larger than 0
	public float accelRate;			// small value, like 0.03

	// For rotation
	public float totalRotation;		// Used for rotation the prefab toward the correct direction
	public float anglePerFrame;		// Degrees of rotation per frame


    public bool isAccelerating = true;
    // Use this for initialization
    
    
    
	void Start () 
	{
		// Grab the starting position from the object's transform
		position = transform.position;
        
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(Input.GetKey(KeyCode.LeftArrow))			// positive rotation
		{
			totalRotation += anglePerFrame;
			direction = Quaternion.Euler (0, 0, anglePerFrame) * direction;
		}
		else if(Input.GetKey(KeyCode.RightArrow))	// negative rotation
		{
			totalRotation -= anglePerFrame;
			direction = Quaternion.Euler (0, 0, -anglePerFrame) * direction;
		}

        // Step 4: Use an acceleration vector
        // Grab a portion of the direction vector and use for constant acceleration
        if (isAccelerating == true)
        {

            acceleration = accelRate * direction;
            // Add acceleration vector to velocity
            velocity += acceleration;

        }
        else
        {
            velocity = velocity * .95f;
        }
		
		// Limit the speed
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		// Add velocity to position
		position += velocity;

		// update the prefab's position
		transform.position = position;

		// Step 3A
		// Rotate the prefab to turn in the direction

      
        if (Input.GetKey(KeyCode.UpArrow))
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
        }
       
       
    }
  
}
