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
    [HideInInspector]
    public Inventory playerInventory;

    public List<CustomerData> customers;

    public float spawnDelay = 10.0f;
    float spawnTimer;

    public int customersServed = 0;
    public int customersSpawned = 0;

    public int maxItemPerOrder = 4; // this number scales as we progress

    [Header("AI Pathfinding")]
    public Transform spawnPoint;
    public Transform entryPoint;
    // A list of all the paths to chairs.
    public List<ChairPath> chairPaths;

    [Header("UI Elements")]
    public Image exclamationMark;
    public Text orderCounter;

    public GameObject orderTicker;
    public GameObject orderTemplate;

    public List<Order> tickerOrders = new List<Order>();

    public GameObject pauseMenu;

    public Text gameTimerText;
    public GameObject gameOverPanel;
    public Text finalScoreText;

    [Header("Game Scoring")]
    public float maxGameTime;
    float gameTime;

    [Header("Audio")]
    public AudioClip entranceClip;
    public AudioClip angryClip;
    public AudioClip happyClip;

    [HideInInspector]
    public bool gameIsPaused = false;
    [HideInInspector]
    public bool gameIsOver = false;

    float globalTimer = 0f;

    int maxAgents = 3;
    [HideInInspector]
    public int currentAgents = 0;
    private void Start()
    {
        playerInventory = player.GetComponent<Inventory>();
        spawnTimer = spawnDelay;

        SpawnCustomer(customers[GenerateRandomSpawn()]);

        gameTime = maxGameTime;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (gameIsOver)
            return;

        SpawnOnTimer();
        orderCounter.text = customersServed + "/" + customersSpawned;
        UpdateGameTimer();


        if (customersServed > 1)
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


        globalTimer += Time.deltaTime;
        if (currentAgents <= 0)
            currentAgents = 0;

        if (globalTimer >= 120)
        {
            maxAgents *= 2;
            globalTimer = 0;
        }
    }

    void SpawnCustomer(CustomerData customer)
    {
        if (currentAgents >= maxAgents)
        {
            spawnTimer = spawnDelay;
            return;
        }


        for (int i = 0; i < chairPaths.Count; i++)
        {
            if (chairPaths[i].isOccupied == false)
            {
                GameObject go = Instantiate(customer.prefab, spawnPoint.position, Quaternion.identity);
                go.GetComponent<WaypointNavigator>().currentPath = chairPaths[i];
                go.GetComponent<WaypointNavigator>().currentPath.isOccupied = true;
                go.GetComponent<CustomerController>().orderID = customersSpawned;
                go.GetComponent<CustomerController>().seatID = go.GetComponent<WaypointNavigator>().currentPath.seatID;
                customersSpawned += 1;
                currentAgents += 1;
                spawnTimer = spawnDelay;
                break;
            }
            Debug.Log("All seats are occupied");
        }
    }

    int GenerateRandomSpawn()
    {
        int num = Random.Range(0, customers.Count);
        Debug.Log(num);
        return num;
    }

    void SpawnOnTimer()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnCustomer(customers[GenerateRandomSpawn()]);
        }
    }

    public void CreateOrderOnTicker(List<FoodItem> order, int id, CustomerController customer)
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
        go.GetComponentInChildren<Text>().text = customer.seatID.ToString();
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

    public void PauseGame(bool state)
    {
        pauseMenu.SetActive(state);
        gameIsPaused = state;
        if (state)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    void UpdateGameTimer()
    {
        gameTime -= Time.deltaTime;
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(gameTime);
        gameTimerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        if (gameTime <= 0)
        {
            gameIsOver = true;
            gameOverPanel.SetActive(true);
            finalScoreText.text = customersServed.ToString();
        }
    }
}