using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : Interactable
{
    public CustomerData customerData;
    public List<FoodItem> currentOrder;
    public int orderID;

    public GameObject speechBubble;
    public GameObject speechBubbleNib;

    WaypointNavigator navigator;

    bool orderTaken = false;
    Image exclamationMark;

    public float orderTimer = 5.0f;
    bool startTimer = false;

    bool isServed;

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
        if (orderTaken == false && navigator.hasReachedChair)
        {
            Debug.Log("Taking order");
            ShowOrder();
            orderTaken = true;
        }
        else
        {
            if (GameManager.instance.playerInventory.currentItem != null && navigator.isLeaving == false)
            {
                if (currentOrder.Contains(GameManager.instance.playerInventory.currentItem))
                {
                    Debug.Log(customerData.catName + " does want " + GameManager.instance.playerInventory.currentItem);

                    // Remove from ticker
                    Order order = GameManager.instance.GetOrderFromTicker(orderID);
                    int itemId = order.GetItemIDFromOrder(GameManager.instance.playerInventory.currentItem);
                    Destroy(order.foodIcons[itemId]);
                    order.currentOrder.RemoveAt(itemId);

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

        if (navigator.hasReachedChair && orderTaken == false)
        {
            startTimer = true;
            ShowExclamationSpeech();
        }

        if (currentOrder.Count == 0)
        {
            if (isServed == false)
            {
                GameManager.instance.customersServed += 1;
                isServed = true;
            }
            
            navigator.isLeaving = true;
        }

        if (startTimer)
        {
            orderTimer -= Time.deltaTime;
            if (GameManager.instance.GetOrderFromTicker(orderID))
            {
                GameManager.instance.GetOrderFromTicker(orderID).timer.text = orderTimer.ToString("00");
            }
            if (orderTimer <= 0f)
            {
                navigator.isLeaving = true;
                if (GameManager.instance.GetOrderFromTicker(orderID))
                {
                    Destroy(GameManager.instance.GetOrderFromTicker(orderID).gameObject);
                }
            }
        }
    }

    void GenerateOrder()
    {
        // First, generate a random number of items to show from the order
        int numberOfItems = GameManager.instance.maxItemPerOrder;

        for (int i = 0; i < numberOfItems; i++)
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
        exclamationMark.enabled = true;
        SetSpeechBubbleState(true);
    }

    void ShowOrder()
    {
        exclamationMark.gameObject.SetActive(false);
        foreach (FoodItem item in currentOrder)
        {
            Instantiate(item.foodIcon, speechBubble.transform.position, Quaternion.identity, speechBubble.transform);
        }
        orderTimer = 30.0f;
        Destroy(speechBubble, 3.0f);
        Destroy(speechBubbleNib, 3.0f);
        Debug.Log("Placing order");
        GameManager.instance.CreateOrderOnTicker(currentOrder, orderID);
        Debug.Log("Order placed");
    }
}