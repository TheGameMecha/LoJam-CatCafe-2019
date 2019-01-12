using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : Interactable
{
    public CustomerData customerData;
    public List<FoodItem> currentOrder;

    void Start()
    {
        currentOrder = customerData.foodOrder;
    }

    public override void Interact()
    {
        if (GameManager.instance.playerInventory.currentItem != null)
        {
            if (currentOrder.Contains(GameManager.instance.playerInventory.currentItem))
            {
                Debug.Log(customerData.catName + " does want " + GameManager.instance.playerInventory.currentItem);
                currentOrder.Remove(GameManager.instance.playerInventory.currentItem);
                GameManager.instance.playerInventory.currentItem = null;
                GameManager.instance.playerInventory.DestroyItem();
            }
            else if (GameManager.instance.playerInventory.currentItem != null)
            {
                Debug.Log(customerData.catName + " does not want that item");
            }
            else
            {
                Debug.Log("You have no item");
            }
        }
    }
}