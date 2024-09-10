using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CursorDesign;
using static CursorData;
using AYellowpaper.SerializedCollections;


public class PlayerController : MonoBehaviour
{

    // Static variable that holds the single instance of the class
    public static PlayerController instance;

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


    [SerializeField] private CursorData _cursorData;

    [SerializeField] private bool _isDragging;


    public bool GetIsDragging() { return _isDragging; }



    // Start is called before the first frame update
    void Start()
    {
        GameStateInstance.instance.TestFunc();

    }


    public void SetCursor(CursorDesign cursorDesign)
    {
        if (_cursorData  && !_isDragging)
        {
            Cursor.SetCursor(_cursorData.GetCursor(cursorDesign), _cursorData.GetCursorOffset(), CursorMode.ForceSoftware);
        }
    }



}
