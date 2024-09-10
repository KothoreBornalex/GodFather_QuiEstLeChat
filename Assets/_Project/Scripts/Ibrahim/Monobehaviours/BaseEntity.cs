using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEntity : MonoBehaviour
{
    private Button ButtonComponent;

    // Start is called before the first frame update
    void Start()
    {
        ButtonComponent = GetComponent<Button>();
        ButtonComponent.onClick.AddListener(Interact);
    }



    private void Interact()
    {
        Debug.Log("I'm THE BUTTON");
    }
}
