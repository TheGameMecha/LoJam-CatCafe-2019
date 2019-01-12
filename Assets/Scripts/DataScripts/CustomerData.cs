using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Customer Data")]
public class CustomerData : ScriptableObject
{
    public string catName;
    public List<FoodItem> foodOrder = new List<FoodItem>();
    public GameObject prefab;
}