using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
    [Header("Base Entity Informations")]
    [SerializeField] private string _entityName;
    [SerializeField] private string _entityDescription;

    [Header("Locker Informations")]
    [SerializeField] private bool _isLocked;
    [SerializeField] private EntityData _keyToUnlock;



    public bool GetIsLocked()
    {
        return _isLocked;
    }
}
