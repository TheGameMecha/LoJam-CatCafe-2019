using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Interactable
{

    public FoodItem itemOnPlate = null;

    public override void Interact()
    {
        // If the player's inventory has an item, remove it and spawn it on the plate
        if (GameManager.instance.playerInventory.currentItem != null)
        {
            itemOnPlate = GameManager.instance.playerInventory.currentItem;
            Instantiate(itemOnPlate.foodInteractable.gameObject, transform.position, Quaternion.identity);


            GameManager.instance.playerInventory.currentItem = null;

        }
    }

}
