using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        playerInventory = player.GetComponent<Inventory>();
    }

}
