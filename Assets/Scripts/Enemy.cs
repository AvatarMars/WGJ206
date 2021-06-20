using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyStates
    {
        NONE,
        LEFT_WANDER,
        RIGHT_WANDER,
        IDLE,
        ATTACK,
        DEATH
    }

    Rigidbody2D rb;
    [SerializeField]
    Transform leftEdge;
    [SerializeField]
    Transform rightEdge;
    [SerializeField]
    float MoveScalar;
    [SerializeField]
    Collider2D AttackTrigger; //Planning ahead and allowing you to define bounds in which wander behaviour is swapped for murder hobo behaviour
    EnemyStates state = EnemyStates.NONE;

    Vector2 moveDirection = Vector2.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int leftOrRight = Random.Range(0, 1); //Flip a coin, which way do we wander?
        if (leftOrRight == 0)
            state = EnemyStates.LEFT_WANDER;
        else
            state = EnemyStates.RIGHT_WANDER;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= leftEdge.position.x) //We have reached the left side, we should wander to the right now
            state = EnemyStates.RIGHT_WANDER;
        if (transform.position.x >= rightEdge.position.x) //And vice versa
            state = EnemyStates.LEFT_WANDER;

        switch (state) //Overengineered way of handling movement based on state :(
        {
            case EnemyStates.LEFT_WANDER:
                {
                    moveDirection = Vector2.left * MoveScalar;
                    break;
                }
            case EnemyStates.RIGHT_WANDER:
                {
                    moveDirection = Vector2.right * MoveScalar;
                    break;
                }
            default: break;
        }

        moveDirection.y = rb.velocity.y;
        rb.velocity = moveDirection; //This moves our boi
    }
}
