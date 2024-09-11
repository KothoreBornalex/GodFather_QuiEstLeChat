using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInfoSystem : MonoBehaviour
{
    public static TextInfoSystem instance;

    Animator uiAnimator;

    [SerializeField] private Image objectImage;
    [SerializeField] private TextMeshProUGUI ObjectName;
    [SerializeField] private TextMeshProUGUI objectDescription;


    private bool textInfoIsOn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        uiAnimator = GetComponent<Animator>();

        textInfoIsOn = false;
    }

    public void TextInfoIn(TestForJM scriptableObject)
    {
        if(textInfoIsOn) { EndAnimation(); }
        else
        {
            objectImage.sprite = scriptableObject.image;
            ObjectName.text = scriptableObject.nameText;
            objectDescription.text = scriptableObject.descriptionText;
            StartAnimation();
        }
    }
    
    void StartAnimation()
    {
        if (uiAnimator != null)
        {
            uiAnimator.SetBool("PlayInfo", true);
            textInfoIsOn = true;   
        }
    }

    void EndAnimation()
    {
        if (uiAnimator != null)
        {
            uiAnimator.SetBool("PlayInfo", false);
            textInfoIsOn = false;
        }
    }

    public void closeTextWindow()
    {
        EndAnimation();
    }
        
}
