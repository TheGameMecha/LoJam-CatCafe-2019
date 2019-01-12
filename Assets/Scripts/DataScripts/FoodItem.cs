using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Food Item")]
public class FoodItem : ScriptableObject
{
    public string foodName_id;
    public GameObject prefab;
    public Food foodInteractable;
}