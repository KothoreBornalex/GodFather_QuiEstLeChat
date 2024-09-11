using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CursorDesign;
using static CursorData;
using AYellowpaper.SerializedCollections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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
    [SerializeField] private RectTransform canvasRectTransform; // The RectTransform of your Canvas

    [SerializeField] private float _dragSpeed = 8.0f;
    private bool _isDragging;
    private BaseEntity _draggedEntity;

    public bool GetIsDragging() { return _isDragging; }
    public float GetDraggingSpeed() { return _dragSpeed; }

    public void StartDragging(BaseEntity EntityToDrag)
    {
        _draggedEntity = EntityToDrag;
    }

    public void StopDragging()
    {
        _draggedEntity = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCursor(CursorDesign.SimpleCursor);
    }


    void Update()
    {
        if (_draggedEntity != null)
        {
            Vector2 mousePosition = Input.mousePosition;

            // Convert the screen space mouse position to local space within the canvas
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, Camera.main, out localPoint);

            // Set the dragged object's position to the local point
            _draggedEntity.targetLocation = localPoint;
        }

    }


    public void SetCursor(CursorDesign cursorDesign)
    {
        if (_cursorData  && !_isDragging)
        {
            Cursor.SetCursor(_cursorData.GetCursor(cursorDesign), _cursorData.GetCursorOffset(), CursorMode.ForceSoftware);
        }
    }


    public void SelectEntity()
    {

    }
}
