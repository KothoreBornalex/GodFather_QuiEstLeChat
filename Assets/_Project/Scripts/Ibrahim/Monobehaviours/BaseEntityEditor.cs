using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(BaseEntity))]
public class BaseEntityEditor : Editor
{

    private BaseEntity baseEntity;
    private Button button;

    private void OnEnable()
    {
        // Cast the target to BaseEntity and store it in the baseEntity field
        baseEntity = (BaseEntity)target;
        button = baseEntity.gameObject.GetComponent<Button>();
    }


    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Set Up Entity"))
        {
            //Debug.Log("Entity Initialized !");
            baseEntity.SetUpEntity(button);

        }

        DrawDefaultInspector();

    }
}
