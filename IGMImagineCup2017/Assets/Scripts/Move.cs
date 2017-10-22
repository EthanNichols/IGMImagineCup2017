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

    public float rammingCoolDown;
    public float ramTime;
    private float ramTimeReset;
    private float resetCoolDown;
    public bool Ramming;

    private GameObject canvas;

	// Use this for initialization
	void Start () {
        resetCoolDown = rammingCoolDown;
        ramTimeReset = ramTime;
        ramTime = 0;

        canvas = GameObject.FindGameObjectWithTag("Canvas");
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

        if (Input.GetKey(KeyCode.E) &&
            rammingCoolDown < 0 &&
            canvas.GetComponent<UIManager>().score >= 100)
        {
            Ramming = true;
            rammingCoolDown = resetCoolDown;
            ramTime = ramTimeReset;
            StartRamming();

            canvas.GetComponent<UIManager>().score -= 100;
        }

        if (ramTime > 0)
        {
            Ramming = true;
            StartRamming();
            ramTime -= Time.deltaTime;
        } else
        {
            Ramming = false;
            rammingCoolDown -= Time.deltaTime;
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
        canvas.GetComponent<UIManager>().score++;
    }

    private void Decellerate()
    {
        //Slow the boat down relative to the velocity
        GetComponent<Rigidbody>().velocity *= .97f;

        //Set the velocity to 0 if it is significantly small
        if (GetComponent<Rigidbody>().velocity.magnitude < .2f) { GetComponent<Rigidbody>().velocity = Vector3.zero; }
    }

    private void StartRamming()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * (speed * 5f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Enemy" &&
            !Ramming)
        {
            canvas.GetComponent<UIManager>().hits++;

            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }
    }
}
