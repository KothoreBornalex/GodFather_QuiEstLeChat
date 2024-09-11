using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testfortext : MonoBehaviour
{
    public TestForJM uiInfoData;

    [SerializeField] private GameObject testForJM;

    public void ButtonPressed()
    {
        TextInfoSystem.instance.TextInfoIn(uiInfoData);
    }

    public void ButtonPressed2()
    {
        testForJM.SetActive(true); 
    }
}
