using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowPlayer : MonoBehaviour
{
    //vars
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position-player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //offset the camera behind the player, by adding to the player's position
        transform.position = player.transform.position + offset;
    }
}
