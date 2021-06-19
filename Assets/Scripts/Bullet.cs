using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float BulletSpeed;
    [SerializeField]
    float BulletLife; //How long before this bullet goes bye-bye?

    Vector2 Direction; //Which way will the bullet speed off towards? (Also must be normalized)
    Rigidbody2D bulletRB;
    float lifeLeft;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        lifeLeft = BulletLife; //Set the life to max time
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = Direction * BulletSpeed;
        if (lifeLeft < 0)
            GameObject.Destroy(gameObject); //See you later, bullet
        lifeLeft -= Time.deltaTime;
    }

    public void SetDirection(Vector2 dir)
    { //You better hand this Vector2 in normalized >:(
        Direction = dir; //That's all, folks!
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for bullet collisions here when there is stuff to hit
    }
}
