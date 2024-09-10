using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Static variable that holds the single instance of the class
    private static PlayerController instance;

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


    // Start is called before the first frame update
    void Start()
    {
        GameStateInstance.instance.TestFunc();
    }


}
