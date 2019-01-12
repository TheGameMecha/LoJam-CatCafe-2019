using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Mathf.Lerp(0f, Input.GetAxis("Horizontal") * moveSpeed, 0.8f),
                                                Mathf.Lerp(0f, Input.GetAxis("Vertical") * moveSpeed, 0.8f));

    }
}
