using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public int orderID;
    public Text timer;
    public List<Image> foodIcons = new List<Image>();
    public List<FoodItem> currentOrder = new List<FoodItem>();

    void Update()
    {
        if (currentOrder.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetItemIDFromOrder(FoodItem item)
    {
        return currentOrder.IndexOf(item);
    }
}