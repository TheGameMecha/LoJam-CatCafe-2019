using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public bool playerInside = false;
    [HideInInspector]
    public PlayerController player;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInside = true;
            player = other.GetComponent<PlayerController>();
            player.ButtonPrompt(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInside = false;
            player.ButtonPrompt(false);
        }
    }

    public virtual void Update()
    {
        if (playerInside && Input.GetButtonDown("Jump"))
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted");
    }
}