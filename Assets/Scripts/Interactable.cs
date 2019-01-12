using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public bool playerInside = false;
    public PlayerController player;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInside = true;
            player = other.GetComponent<PlayerController>();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInside = false;
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