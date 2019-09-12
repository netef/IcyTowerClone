using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private float playerSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerSpeed = GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y;
    }

    void FixedUpdate()
    {
        if (playerSpeed > 0)
            rb.velocity = new Vector2(0, -playerSpeed);
        else
            rb.velocity = Vector2.zero;
    }
}