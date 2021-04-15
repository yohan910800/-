using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0, 0, -10);
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x+offset.x,player.position.y+
            offset.y,offset.z);

        
    }
}
