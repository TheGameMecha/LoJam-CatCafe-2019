using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Interactable
{

    public FoodItem itemOnPlate = null;

    GameObject itemOnPlateInteractable;

    public override void Interact()
    {
        // If the player's inventory has an item, remove it and spawn it on the plate
        if (GameManager.instance.playerInventory.currentItem != null)
        {
            itemOnPlate = GameManager.instance.playerInventory.currentItem;
            itemOnPlateInteractable = 
                Instantiate(itemOnPlate.foodInteractable.gameObject, transform.position, Quaternion.identity);


            GameManager.instance.playerInventory.currentItem = null;
            Destroy(GameManager.instance.playerInventory.itemGameObject);
        }
        // If the player instead wants to pick up the item ON the plate
        else if (GameManager.instance.playerInventory.currentItem == null && itemOnPlate != null)
        {
            GameManager.instance.playerInventory.currentItem = itemOnPlate;
            GameManager.instance.playerInventory.SpawnItem();
            itemOnPlate = null;
            Destroy(itemOnPlateInteractable);
        }
    }
}
