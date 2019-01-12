using System.Collections;
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

    private void Start()
    {
        playerInventory = player.GetComponent<Inventory>();

        SpawnCustomer(customers[0]);
    }

    void Update()
    {
        ManageAIAgents();
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
}