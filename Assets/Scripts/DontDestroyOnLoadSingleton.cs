using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadSingleton : MonoBehaviour
{
    public static DontDestroyOnLoadSingleton instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}