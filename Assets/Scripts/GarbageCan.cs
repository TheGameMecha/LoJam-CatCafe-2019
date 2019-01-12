using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCan : Interactable
{
    public override void Interact()
    {
        // If the player's inventory has an item, remove it and spawn it on the plate
        if (GameManager.instance.playerInventory.currentItem != null)
        {
            GameManager.instance.playerInventory.currentItem = null;
            GameManager.instance.playerInventory.DestroyItem();
        }
    }
}
