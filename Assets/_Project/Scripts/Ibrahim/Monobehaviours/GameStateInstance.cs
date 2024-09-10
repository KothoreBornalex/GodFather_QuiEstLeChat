using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateInstance : MonoBehaviour
{
    // Static variable that holds the single instance of the class
    public static GameStateInstance instance;

    // Ensures that the instance is not duplicated
    private void Awake()
    {
        // Check if there is already an instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional, if you want to keep it across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    public void TestFunc()
    {
        Debug.Log("TTTTTTEEEESSSSTTTT");
    }
}
