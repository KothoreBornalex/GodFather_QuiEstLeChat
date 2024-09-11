using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
    [Header("Base Entity Informations")]
    [SerializeField] private string _entityName;
    [SerializeField] private string _entityDescription;
    [SerializeField] private Sprite _entitySprite;

    [Space(15)]

    [Header("Locker Informations")]
    [SerializeField] private bool _isLocked;
    [SerializeField] private EntityData _keyToUnlock;


     
    public string GetEntityName()
    {
        return _entityName;
    }

    public string GetEntityDescription()
    {
        return _entityDescription;
    }

    public bool GetIsLocked()
    {
        return _isLocked;
    }

    public EntityData GetKeyToUnlock()
    {
        return _keyToUnlock;
    }

    public Sprite GetEntitySprite()
    {
        return _entitySprite;
    }
}
