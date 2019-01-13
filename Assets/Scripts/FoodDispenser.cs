using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispenser : Interactable
{

    public FoodItem dispensedFood;

    public override void Interact()
    {
        if (GameManager.instance.playerInventory.currentItem == null)
        {
            GameManager.instance.playerInventory.currentItem = dispensedFood;
            GameManager.instance.playerInventory.SpawnItem();
            player.GetComponent<CharacterAnimator>().InteractTrigger();
        }
    }
}
