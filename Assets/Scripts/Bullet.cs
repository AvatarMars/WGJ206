using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Note: we are rotating the bullet prefab upon instantiation, so it does not need to contain a direction variable

    [SerializeField]
    float BulletSpeed;
    [SerializeField]
    float BulletLife; //How long before this bullet is destroyed in seconds?

    Rigidbody2D bulletRB;
    float lifeLeft;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        lifeLeft = BulletLife; //Set the life to max time
        bulletRB.AddForce(transform.right * BulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeLeft < 0)
        {
            GameObject.Destroy(gameObject); //See you later, bullet
        }

        lifeLeft -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for bullet collisions here when there is stuff to hit
    }
}
