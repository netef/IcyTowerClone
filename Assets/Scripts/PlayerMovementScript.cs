using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject star;
    private Rigidbody2D rb;
    public float jumpForce;
    private bool jump = false;

    [SerializeField]
    private float speed = 0;

    private float acceleration = 30f;
    private float maxSpeed = 20f;
    private float minSpeed = -20f;


    private float inputX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
            jump = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (jump)
            Jump();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("wall"))
            speed = -speed;
    }

    void Movement()
    {
        if (inputX > 0 && speed < maxSpeed)
            speed += acceleration * Time.deltaTime;
        else if (inputX < 0 && speed > minSpeed)
            speed -= acceleration * Time.deltaTime;
        else if (inputX == 0 && speed != 0)
        {
            if (speed > 0)
            {
                speed -= acceleration * Time.deltaTime;
                if (speed < 0.5)
                    speed = 0;
            }
            else if (speed < 0)
            {
                speed += acceleration * Time.deltaTime;
                if (speed > -0.5)
                    speed = 0;
            }
        }
    }

    void Jump()
    {
        jump = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce + (Mathf.Abs(speed)) / 2);
        if (Mathf.Abs(speed) > 15f)
            Stars();
    }

    void Stars()
    {
        StartCoroutine(Make());
    }

    IEnumerator Make()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.001f);
            GameObject starCreated = Instantiate(star, transform.position, Quaternion.identity);
            starCreated.AddComponent<Rigidbody2D>();
            Rigidbody2D starRb = starCreated.GetComponent<Rigidbody2D>();
            float rand = Random.Range(-10f, 10);
            starRb.velocity = new Vector2(rand, 10);
            Destroy(starCreated, 3f);
        }
    }
}