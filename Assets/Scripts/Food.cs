using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    public FoodItem foodItem;

    public override void Interact()
    {
        if (GameManager.instance.playerInventory.currentItem == null)
        {
            GameManager.instance.playerInventory.currentItem = foodItem;
            GameManager.instance.playerInventory.SpawnItem();
            player.GetComponent<CharacterAnimator>().InteractTrigger();
            Destroy(gameObject);
        }
    }
}