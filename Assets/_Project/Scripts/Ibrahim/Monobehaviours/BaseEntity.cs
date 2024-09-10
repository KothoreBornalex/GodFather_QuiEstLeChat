using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseEntity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Data")] 
    [SerializeField] private EntityData _entityData;

    private Button _buttonComponent;

            

    void Awake()
    {
        _buttonComponent = GetComponent<Button>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _buttonComponent.onClick.AddListener(OnClick);
    }



    private void OnClick()
    {
        Interact();
    }


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

        if (_entityData.GetIsLocked())
        {
            if (PlayerController.instance.GetIsDragging())
            {
                Use();
            }
        }
        
    }




    private void Interact()
    {
        Debug.Log("I'm THE BUTTON");
    }

    private void Use()
    {
        Debug.Log("I'm Being Used");
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