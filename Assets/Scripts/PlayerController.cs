using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        rb.velocity = new Vector2(Mathf.Lerp(0f, horizontal * moveSpeed, 0.8f),
                                                Mathf.Lerp(0f, vertical * moveSpeed, 0.8f));

        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

}