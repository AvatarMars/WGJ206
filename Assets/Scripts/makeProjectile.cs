using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeProjectile : MonoBehaviour
{
    public GameObject toShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject instance = Instantiate(toShoot, transform.position, transform.rotation, null);
            //.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
        }
    }
}
