using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D heroRB;
    [SerializeField]
    float MoveScalar;
    [SerializeField]
    float JumpPower;
    [SerializeField]
    GameObject ShootyStick;
    [SerializeField]
    Transform ShootyEnd; //The shooty part of the shooty stick
    [SerializeField]
    Transform HeroFeet; //For getting the raycast's origin when ground checking
    [SerializeField]
    LayerMask GroundCheckLayers;
    [SerializeField]
    float GroundCheckDistance;
    [SerializeField]
    float FallGravity; //To help fix floatiness
    [SerializeField]
    float ExtraGavitySpeed; //The speed which you must fall under before the extra gravity hits
    [SerializeField]
    GameObject BulletPrefab;
    [SerializeField]
    float ShootCooldown;

    Vector2 movement = Vector2.zero;
    Vector2 GunRightFacing;
    bool grounded = false; //Assume we are floating when the script starts and update in OnCollisionEnter
    float currentCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        GunRightFacing = ShootyStick.transform.localPosition; //The player prefab begins facing right, so we copy that value
    }

    // Update is called once per frame
    void Update()
    {
        //Move inputs get collected and applied first
        float horizontal = Input.GetAxis("Horizontal");
        movement.x = horizontal * MoveScalar;
        movement.y = heroRB.velocity.y;
        heroRB.velocity = movement;

        RaycastHit2D hit = Physics2D.CircleCast(HeroFeet.position, 0.75f, Vector2.down, GroundCheckDistance, GroundCheckLayers.value);

        if (hit.transform != null)
        {
            if (hit.transform.tag == "Ground")
            {
                grounded = true;
            }
        }
        else
            grounded = false;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            heroRB.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse); //Yeeeeet!
        }

        if (heroRB.velocity.y < ExtraGavitySpeed) //We have jumped and now we need some extra gravity oomph
            heroRB.gravityScale = FallGravity;
        else
            heroRB.gravityScale = 1.5f;


        //Set the gun's facing relative to movement's x
        if (movement.x < 0)
        {
            ShootyStick.transform.localPosition = new Vector2(GunRightFacing.x * -1, GunRightFacing.y); //Orientate towards the left
            ShootyStick.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (movement.x > 0)
        {
            ShootyStick.transform.localPosition = GunRightFacing; //Match the original facing
            ShootyStick.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        //Now its time for bullet spawning
        if (currentCooldown <= 0 && Input.GetButtonDown("Fire1"))
        {
            Bullet bullet = GameObject.Instantiate(BulletPrefab, ShootyEnd.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.SetDirection(ShootyEnd.right); //Set the bullet to fire in the direction of the gun's facing
            currentCooldown = ShootCooldown;
        }
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;
    }
}
