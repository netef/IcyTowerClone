using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Transform playerPosition;
    private float playerSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerSpeed = GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y;
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (playerSpeed > 0 && playerPosition.position.y >= 1)
            rb.velocity = new Vector2(0, -playerSpeed * 2);
        else
            rb.velocity = Vector2.zero;
    }
}