using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform m_cannonRot;
	public Transform m_muzzle;
	public GameObject m_shotPrefab;

    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    


	// Use this for initialization
	void Start () 
	{
	
	}

    // Update is called once per frame
    void Update()
    {
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        m_cannonRot.transform.Rotate(v, h, 0);
        /*
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			m_cannonRot.transform.Rotate(Vector3.up, -Time.deltaTime * 100f);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			m_cannonRot.transform.Rotate(Vector3.up, Time.deltaTime * 100f);
		}
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
            GameObject.Destroy(go, 3f);
        }
    }

	}


