using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CodeSystem : MonoBehaviour
{


    public static CodeSystem instance;

    // Ensures that the instance is not duplicated
    private void Awake()
    {
        // Check if there is already an instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional, if you want to keep it across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }


    private GameObject _entityToUnlock;


    [SerializeField] private List<TextMeshProUGUI> uiTexts;

    [SerializeField] private TextMeshProUGUI resultText;

    private List<int> chiffres = new List<int>(); 
    private List<int> targetDigits = new List<int>();

    [SerializeField] private int targetNumber;

    void Start()
    {
        InitializeSystem();
    }

    private void InitializeSystem()
    {
        chiffres.Clear();
        targetDigits.Clear();

        int tempTarget = targetNumber;
        while (tempTarget > 0)
        {
            targetDigits.Insert(0, tempTarget % 10); 
            tempTarget /= 10;
        }

        
        if (uiTexts.Count != targetDigits.Count)
        {
            Debug.LogError("Le nombre de TextMeshProUGUI doit correspondre au nombre de chiffres du nombre cible !");
            return;
        }

        for (int i = 0; i < targetDigits.Count; i++)
        {
            chiffres.Add(0); 
            uiTexts[i].text = "0"; 
        }
    }


    public void StartUnlockPaswword(int password, GameObject entity)
    {
        gameObject.SetActive(true);
        targetNumber = password;
        _entityToUnlock = entity;

        InitializeSystem();
    }


    public void OnIncrementButtonClick(int index)
    {
        if (index >= 0 && index < chiffres.Count)
        {
            chiffres[index]++;

            if (chiffres[index] > 9)
            {
                chiffres[index] = 0;
            }

            uiTexts[index].text = chiffres[index].ToString();

        }
    }

    public void OnDecrementButtonClick(int index)
    {
        if (index >= 0 && index < chiffres.Count)
        {
            chiffres[index]--;

            if (chiffres[index] < 0)
            {
                chiffres[index] = 9;
            }

            uiTexts[index].text = chiffres[index].ToString();

        }
    }

    private bool CheckCombination()
    {
        for (int i = 0; i < chiffres.Count; i++)
        {
            if (chiffres[i] != targetDigits[i])
            {
                resultText.text = "Code incorect !"; 
                return false;
            }
        }

        resultText.text = "Tu as gagné !";

        return true;
    }

    private void ResetNumbers()
    {
        for (int i = 0; i < chiffres.Count; i++)
        {
            chiffres[i] = 0; 
            uiTexts[i].text = "0"; 
        }

        resultText.text = ""; 
    }
    

    public void CheckButton()
    {
        if (CheckCombination())
        {
            if(_entityToUnlock) _entityToUnlock.GetComponent<BaseEntity>().StartDisappear();

            gameObject.SetActive(false);
        }

        ResetNumbers();
    }


}
