using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : Interactable
{
    public CustomerData customerData;
    public List<FoodItem> currentOrder;

    public GameObject speechBubble;
    public GameObject speechBubbleNib;

    WaypointNavigator navigator;

    bool orderTaken = false;
    Image exclamationMark;

    void OnEnable()
    {
        currentOrder = new List<FoodItem>();
        navigator = GetComponent<WaypointNavigator>();
        SetSpeechBubbleState(false);
        exclamationMark = Instantiate(GameManager.instance.exclamationMark, speechBubble.transform.position, Quaternion.identity, speechBubble.transform);

        GenerateOrder();
    }

    public override void Interact()
    {
        if (orderTaken == false)
        {
            ShowOrder();
            orderTaken = true;
        }
        else
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

    public override void Update()
    {
        base.Update();

        if (navigator.hasReachedChair)
        {
            ShowExclamationSpeech();
        }
    }

    void GenerateOrder()
    {
        // First, generate a random number of items to show from the order
        int numberOfItems = Random.Range(1, GameManager.instance.maxItemPerOrder);

        for (int i = 0; i < numberOfItems - 1; i++)
        {
            currentOrder.Add(customerData.foodOrder[i]);
        }
    }

    void SetSpeechBubbleState(bool state)
    {
        speechBubble.SetActive(state);
        speechBubbleNib.SetActive(state);
    }

    void ShowExclamationSpeech()
    {
        exclamationMark.gameObject.SetActive(true);
        SetSpeechBubbleState(true);
    }

    void ShowOrder()
    {
        exclamationMark.gameObject.SetActive(false);
        foreach (FoodItem item in currentOrder)
        {
            Instantiate(item.foodIcon, speechBubble.transform.position, Quaternion.identity, speechBubble.transform);
        }
    }
}