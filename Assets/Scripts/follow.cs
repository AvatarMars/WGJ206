using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform toFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = toFollow.position;
    }
}
