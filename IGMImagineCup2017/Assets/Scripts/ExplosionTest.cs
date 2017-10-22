using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTest : MonoBehaviour {
    public GameObject Explosion;

	// Use this for initialization
	void Start () {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Instantiate(Explosion, hit.point, Quaternion.identity);
        }

    }
}
