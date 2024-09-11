using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseEntity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Data")]

    [Expandable]
    [SerializeField] private EntityData _entityData;

    private Button _buttonComponent;
    private bool _readyToBeDragged;
    private bool _wasDragged;


    [HideInInspector] public Vector2 targetLocation;
    private RectTransform _rectTransform;

    void Awake()
    {
        _buttonComponent = GetComponent<Button>();

        _rectTransform = GetComponent<RectTransform>();
    }


    // Start is called before the first frame update
    void Start()
    {
        targetLocation = _rectTransform.anchoredPosition;
    }


    void Update()
    {
        if (_rectTransform.anchoredPosition != targetLocation)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, targetLocation, Time.deltaTime * PlayerController.instance.GetDraggingSpeed());
        }
    }



    #region Pointers Detection

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerController.instance.SetCursor(CursorDesign.HoverCursor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerController.instance.SetCursor(CursorDesign.SimpleCursor);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_entityData.GetCanBeDragged()) return;

        _readyToBeDragged = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_readyToBeDragged)
        {
            _readyToBeDragged = false;

            PlayerController.instance.StopDragging();
            PlayerController.instance.SetCursor(CursorDesign.SimpleCursor);
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!PlayerController.instance.GetIsDragging() && _readyToBeDragged)
        {
            _wasDragged = true;
            PlayerController.instance.StartDragging(this);
            PlayerController.instance.SetCursor(CursorDesign.DraggedCursor);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_wasDragged)
        {
            _wasDragged = false;
        }
        else
        {
            //Debug.Log("Read Entity Description");

            Interact();
            
        }

        StartCoroutine(TriggerInteractCursor());

    }

    #endregion

    IEnumerator TriggerInteractCursor()
    {
        PlayerController.instance.SetCursor(CursorDesign.SimpleCursor);

        // Wait for 2 seconds
        yield return new WaitForSeconds(0.15f);

        PlayerController.instance.SetCursor(CursorDesign.HoverCursor);
    }

    private void Interact()
    {
        TextInfoSystem.instance.TextInfoIn(_entityData);
    }




    public EntityData GetEntityData()
    {
        return _entityData;
    }


    public void SetUpEntity(Button button)
    {

        // Check if _entityData exists
        if (!_entityData) return;


        button.image.sprite = _entityData.GetEntitySprite();

        EditorUtility.SetDirty(button.image);
        Canvas.ForceUpdateCanvases();
    }
}