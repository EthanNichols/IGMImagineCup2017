using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //The speed of the bullet
    public int speed;

    //The initial direction of the bullet
    public Vector3 direction;
    public GameObject target;

    public Vector3 startingVelocity;

    // Use this for initialization
    void Start()
    {
        transform.position += transform.forward * 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            Vector3 curPos = new Vector3(transform.position.x, 0, transform.position.y);
            Vector3 tarPos = new Vector3(target.transform.position.x, 0, target.transform.position.y);

            transform.Rotate(Vector3.RotateTowards(curPos, tarPos, speed, speed));
        }
    }
}
