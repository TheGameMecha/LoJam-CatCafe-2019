using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public GameObject buttonPrompt;

    CharacterAnimator animator;
    Inventory inventory;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<CharacterAnimator>();
        inventory = GetComponent<Inventory>();

        if (buttonPrompt == null)
            Debug.LogError("Button Prompt isn't hooked up in the player controller. Yell at Scott");

        ButtonPrompt(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            GameManager.instance.gameIsPaused = !GameManager.instance.gameIsPaused;
            GameManager.instance.PauseGame(GameManager.instance.gameIsPaused);
        }

        if (GameManager.instance.gameIsPaused)
            return;

        if (GameManager.instance.gameIsOver)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        rb.velocity = new Vector2(Mathf.Lerp(0f, horizontal * moveSpeed, 0.8f),
                                                Mathf.Lerp(0f, vertical * moveSpeed, 0.8f));

        if (inventory.currentItem != null)
        {
            animator.SetFood(true);
        }
        else
        {
            animator.SetFood(false);
        }

        if (rb.velocity.magnitude > 0.1)
        {
            animator.SetWalk(true);
        }
        else
        {
            animator.SetWalk(false);
        }

        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ButtonPrompt(bool state)
    {
        buttonPrompt.SetActive(state);
    }


}