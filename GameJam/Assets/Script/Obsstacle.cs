using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsstacle : MonoBehaviour
{
    Rigidbody2D rb;

     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.gravityScale=1;
        }
    }
}
