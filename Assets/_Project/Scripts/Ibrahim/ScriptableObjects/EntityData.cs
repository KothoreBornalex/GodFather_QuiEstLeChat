using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[Serializable]
public enum EntityTypes
{
    Default,
    Key,
    Lock,
    Ingredient,
    PasswordLock
};

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
    [Header("Base Entity Informations")]
    [SerializeField] private string _entityName;
    [SerializeField] private string _entityDescription;
    [SerializeField] private Sprite _entitySprite;
    [SerializeField] private EntityTypes _entityType;
    [SerializeField] private GameObject _entityPrefab;
    [SerializeField] private bool _canBeDragged;
    [SerializeField] private Color _entityColor = Color.white;

    [Space(15)]

    [Header("Locker Informations")]
    [SerializeField] private EntityData _keyToUnlock;
    [SerializeField] private int _password;



    [Header("Cook Informations")]
    [SerializeField] private EntityData _fullEntity;
    [SerializeField] private EntityData _ingredientToFill;


    public int GetPassword()
    {
        return _password;
    }
    public string GetEntityName()
    {
        return _entityName;
    }

    public Color GetEntityColor()
    {
        return _entityColor;
    }
    public EntityTypes GetEntityType()
    {
        return _entityType;
    }

    public string GetEntityDescription()
    {
        return _entityDescription;
    }


    public EntityData GetKeyToUnlock()
    {
        return _keyToUnlock;
    }

    public EntityData GetFullEntity()
    {
        return _fullEntity;
    }

    public EntityData GetIngredientToFill()
    {
        return _ingredientToFill;
    }

    public Sprite GetEntitySprite()
    {
        return _entitySprite;
    }

    public GameObject GetEntityPrefab()
    {
        return _entityPrefab;
    }

    public bool GetCanBeDragged()
    {
        return _canBeDragged;
    }
}
