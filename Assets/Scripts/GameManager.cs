﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public PlayerController player;
    public Inventory playerInventory;

    public List<CustomerData> customers;

    public int customersServed = 0;
    public int customersSpawned = 0;

    public int maxItemPerOrder = 1; // this number scales as we progress

    [Header("AI Pathfinding")]
    public Transform spawnPoint;
    public Transform entryPoint;
    // A list of all the paths to chairs.
    public List<ChairPath> chairPaths;

    [Header("UI Elements")]
    public Image exclamationMark;

    public GameObject orderTicker;
    public GameObject orderTemplate;

    public List<Order> tickerOrders = new List<Order>();

    private void Start()
    {
        playerInventory = player.GetComponent<Inventory>();

        SpawnCustomer(customers[0]);
    }

    void Update()
    {
        ManageAIAgents();

        if (customersServed > 5)
        {
            maxItemPerOrder = 2;
        }
        if (customersServed > 13)
        {
            maxItemPerOrder = 3;
        }
        if (customersServed > 25)
        {
            maxItemPerOrder = 4;
        }
    }

    void SpawnCustomer(CustomerData customer)
    {
        for (int i = 0; i < chairPaths.Count; i++)
        {
            if (chairPaths[i].isOccupied == false)
            {
                GameObject go = Instantiate(customer.prefab, spawnPoint.position, Quaternion.identity);
                go.GetComponent<WaypointNavigator>().currentPath = chairPaths[i];
                go.GetComponent<WaypointNavigator>().currentPath.isOccupied = true;
                go.GetComponent<CustomerController>().orderID = customersSpawned;
                customersSpawned += 1;
                break;
            }
            Debug.Log("All seats are occupied");
        }
    }

    void ManageAIAgents()
    {
        SpawnCustomer(customers[0]);
    }

    public void CreateOrderOnTicker(List<FoodItem> order, int id)
    {
        GameObject go = Instantiate(orderTemplate, orderTicker.transform, false);
        int i = 0;

        foreach (FoodItem item in order)
        {
            Debug.Log("Creating Icon and assigning it to order");
            Image icon = Instantiate(item.foodIcon, go.transform.position, Quaternion.identity, go.GetComponentInChildren<Image>().rectTransform);
            go.GetComponent<Order>().foodIcons.Add(icon);
            i++;
        }
        Debug.Log("Assigned order on ticker");
        go.GetComponent<Order>().currentOrder = order;
        go.GetComponent<Order>().orderID = id;
        go.GetComponentInChildren<Text>().text = id.ToString();
        tickerOrders.Add(go.GetComponent<Order>());
    }

    public Order GetOrderFromTicker(int id)
    {
        foreach (Order order in tickerOrders)
        {
            if (order.orderID == id)
                return order;
        }
        return null;
    }
}