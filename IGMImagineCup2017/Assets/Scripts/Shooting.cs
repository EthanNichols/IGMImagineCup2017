using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    //The bullet prefab
    public GameObject bullet;

    //The max distance to shoot
    //The angles that the ship shoots from
    public float shootingRadius;
    public float shootingAngle;

    //The chance that it will hit the target
    //The amount of bullet that will fire
    public float hitChance;
    public int bulletCount;

    //The delay between each shot
    //The reset for the shooting delay
    public float shootingDelay;
    private float shootingReset;
    private int bulletsShot;

    //List of targets that can be shot at
    private List<GameObject> targets = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        shootingReset = shootingDelay;
        hitChance = 1;

        AddTargets();

        bulletsShot = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //Reduce the amount of time tile the next bullet
        if (shootingDelay > 0) { shootingDelay -= Time.deltaTime; }

        //Test if the ship can shoot
        if (shootingDelay <= 0)
        {
            //Loop through all of the possible targets that can be shot at
            foreach (GameObject target in targets)
            {
                //Test if the possible target is in range
                if (Vector3.Distance(transform.position, target.transform.position) < shootingRadius)
                {
                    //Shoot at the target
                    //Reset the shooting delay
                    if (++bulletsShot >= bulletCount)
                    {
                        Shoot(target);
                        shootingDelay = shootingReset;
                        bulletsShot = 0;
                    } else
                    {
                        shootingDelay += Time.deltaTime * 2;
                        Shoot(target);
                    }
                    break;
                }
            }
        }
    }

    public void AddTargets()
    {
        //Clear all of the targets in the list
        targets.Clear();

        //Set all of the player targets to enemy ships
        if (gameObject.tag == "Player")
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                targets.Add(enemy);
            }

            //Set the enemy ship target to the player
        }
        else
        {
            targets.Add(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    private void Shoot(GameObject target)
    {
        //Random starting seed
        Random.InitState((int)System.DateTime.Now.Ticks);

        //Fire the amount of bullets the ship can shoot at once
        for (int i = 0; i < bulletCount; i++)
        {
            //Create a new bullet, make sure it doesn't initially collide with the shooter
            GameObject firedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            Vector3 direction = (target.transform.position - transform.position).normalized;
            firedBullet.transform.position += direction * (GetComponent<Renderer>().bounds.extents.x * 1.5f);

            //Determine if it will hit the target or not
            if (Random.Range(0f, 1f) < hitChance)
            {
                firedBullet.GetComponent<Bullet>().hitTarget = true;
            }

            firedBullet.GetComponent<Bullet>().startingVelocity = gameObject.GetComponent<Rigidbody>().velocity.normalized;

            //Set the bullet's target and the direction it is going in
            firedBullet.GetComponent<Bullet>().target = target;
            firedBullet.GetComponent<Bullet>().direction = target.transform.position - transform.position;
        }
    }
}
