using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public FoodItem currentItem = null;

    public Transform foodSpawnPoint;

    public void SpawnItem()
    {
        Instantiate(currentItem.prefab, foodSpawnPoint.position, Quaternion.identity, foodSpawnPoint);
    }
}
