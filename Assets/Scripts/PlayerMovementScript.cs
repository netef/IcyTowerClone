using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject star;
    public GameObject GFX;
    private Rigidbody2D rb;
    public float jumpForce;
    private bool jump = false;
    private bool stars = false;
    private bool isGrounded = false;

    [SerializeField]
    private float speed = 0;

    private float acceleration = 30f;
    private float maxSpeed = 20f;
    private float minSpeed = -20f;


    private float inputX;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GameObject.Find("GFX").GetComponent<Animator>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        Movement();

        anim.SetFloat("velocity", Mathf.Abs(speed));
        anim.SetBool("isGrounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            jump = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (jump)
            Jump();
        if (stars)
            GFX.transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
        else
            GFX.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
            speed = -speed;
        else if (collision.gameObject.CompareTag("platform"))
            isGrounded = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
            isGrounded = false;
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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce + (Mathf.Abs(speed)) / 2.5f);
        if (Mathf.Abs(speed) > 15f && !stars)
            Stars();
    }

    void Stars()
    {
        stars = true;
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
        stars = false;
    }
}