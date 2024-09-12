using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BaseEntity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Data")]

    [Expandable]
    [SerializeField] private EntityData _entityData;


    private Button _buttonComponent;
    private bool _readyToBeDragged;
    private bool _wasDragged;
    private Animator _animator;

    [HideInInspector] public Vector2 targetLocation;
    private RectTransform _rectTransform;

    void Awake()
    {
        _buttonComponent = GetComponent<Button>();
        _animator = GetComponent<Animator>();
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

            CheckForUse();
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

            ReCookescription();
        }

        StartCoroutine(TriggerInteractCursor());

    }

    #endregion
    public void CheckForUse()
    {

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
        };

        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        Debug.Log(results.Count);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject) continue;




            if (result.gameObject.GetComponent<BaseEntity>())
            {
                BaseEntity baseEntity = result.gameObject.GetComponent<BaseEntity>();

                switch (baseEntity.GetEntityData().GetEntityType())
                {
                    case EntityTypes.Default:
                        break;

                    case EntityTypes.Lock:
                        baseEntity.Unlock(_entityData);
                        break;

                    case EntityTypes.Key:
                        break;

                    case EntityTypes.Ingredient:
                        Debug.Log("Coooooook");
                        baseEntity.Cook(this);
                        break;
                }

            }

        }

    }


    IEnumerator TriggerInteractCursor()
    {
        PlayerController.instance.SetCursor(CursorDesign.SimpleCursor);

        // Wait for 2 seconds
        yield return new WaitForSeconds(0.15f);

        PlayerController.instance.SetCursor(CursorDesign.HoverCursor);
    }



    private void ReCookescription()
    {
        TextInfoSystem.instance.TextInfoIn(_entityData);
    }


    private void Cook(BaseEntity externIngredient)
    {
        if (_entityData.GetIngredientToFill() == externIngredient.GetEntityData())
        {
            //EntityData NewEntityData = test;
            GameObject entityInstance = Instantiate(_entityData.GetFullEntity().GetEntityPrefab(), transform.position, transform.rotation, GameStateInstance.instance.CanvasMap.transform);

            entityInstance.GetComponent<RectTransform>().anchoredPosition = _rectTransform.anchoredPosition;
            entityInstance.GetComponent<RectTransform>().rotation = _rectTransform.rotation;


            entityInstance.GetComponent<BaseEntity>().SetUpEntityWithData(_entityData.GetFullEntity());


            StartDisappear();
            externIngredient.StartDisappear();

            /*Destroy(gameObject);
            Destroy(externIngredient.gameObject);*/
        }
        
    }


    private void Unlock(EntityData keyData)
    {
        if (_entityData.GetEntityType() == EntityTypes.Lock)
        {
            if (_entityData.GetKeyToUnlock() == keyData)
            {
                Debug.Log("Unlock " + _entityData.GetEntityName());
                StartDisappear();
            }
        }
    }


    private void StartDisappear()
    {
        _animator.SetTrigger("disappear");
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


    public void SetUpEntityWithData(EntityData entityData)
    {
        // Check if _entityData exists
        if (!entityData) return;

        _entityData = entityData;

        _buttonComponent = GetComponent<Button>();

        _buttonComponent.image.sprite = _entityData.GetEntitySprite();
    }
}