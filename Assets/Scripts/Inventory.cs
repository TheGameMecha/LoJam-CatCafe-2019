using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public FoodItem currentItem = null;
    public Transform foodSpawnPoint;

    [HideInInspector]
    public GameObject itemGameObject;

    public void SpawnItem()
    {
        itemGameObject = Instantiate(currentItem.prefab, foodSpawnPoint.position, Quaternion.identity, foodSpawnPoint);
    }

    public void DestroyItem()
    {
        Destroy(itemGameObject);
    }
}
