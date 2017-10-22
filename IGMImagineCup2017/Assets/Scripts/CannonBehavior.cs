using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour
{

    //public Transform m_cannonRot;
    //public Transform m_muzzle;
    public GameObject m_shotPrefab;

    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;

    public GameObject setTarget;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet(setTarget);
            setTarget = null;
        }
    }

    public void FireBullet(GameObject target)
    {
        GameObject go = Instantiate(m_shotPrefab, transform.position, Quaternion.identity);

        float click = (Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 96);

        go.transform.rotation = transform.rotation;

        go.GetComponent<Bullet>().target = target;
        go.transform.Rotate(0, click, 0);
        go.transform.position = transform.position;
        go.GetComponent<Bullet>().speed = 100;
        GameObject.Destroy(go, 3f);
    }
}


