using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Food Item")]
public class FoodItem : ScriptableObject
{
    public string foodName_id;
    public GameObject prefab;
    public Food foodInteractable;

    public Image foodIcon;
}