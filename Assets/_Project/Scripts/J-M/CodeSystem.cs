using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CodeSystem : MonoBehaviour
{
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
        // Vider les listes
        chiffres.Clear();
        targetDigits.Clear();

        // Extraire les chiffres du nombre cible
        int tempTarget = targetNumber;
        while (tempTarget > 0)
        {
            targetDigits.Insert(0, tempTarget % 10); // Ajoute chaque chiffre du nombre cible
            tempTarget /= 10;
        }

        // Vérifie que le nombre de champs TextMeshPro correspond au nombre de chiffres du target
        if (uiTexts.Count != targetDigits.Count)
        {
            Debug.LogError("Le nombre de TextMeshProUGUI doit correspondre au nombre de chiffres du nombre cible !");
            return;
        }

        // Initialiser la liste des chiffres avec des 0
        for (int i = 0; i < targetDigits.Count; i++)
        {
            chiffres.Add(0); // Initialise les chiffres à 0
            uiTexts[i].text = "0"; // Met à jour le texte de l'UI à 0
        }
    }

    public void OnIncrementButtonClick(int index)
    {
        if (index >= 0 && index < chiffres.Count)
        {
            // Incrémente le chiffre associé à l'index
            chiffres[index]++;

            // Si le chiffre est supérieur à 9, il repart à 0
            if (chiffres[index] > 9)
            {
                chiffres[index] = 0;
            }

            // Met à jour l'UI
            uiTexts[index].text = chiffres[index].ToString();

            // Vérifie la combinaison après chaque clic
        }
    }

    public void OnDecrementButtonClick(int index)
    {
        if (index >= 0 && index < chiffres.Count)
        {
            // Décrémente le chiffre associé à l'index
            chiffres[index]--;

            // Si le chiffre est inférieur à 0, il repart à 9
            if (chiffres[index] < 0)
            {
                chiffres[index] = 9;
            }

            // Met à jour l'UI
            uiTexts[index].text = chiffres[index].ToString();

            // Vérifie la combinaison après chaque clic
        }
    }

    private void CheckCombination()
    {
        // Compare chaque chiffre avec le chiffre correspondant du nombre cible
        for (int i = 0; i < chiffres.Count; i++)
        {
            if (chiffres[i] != targetDigits[i])
            {
                resultText.text = "Code incorect !"; // Remet le message si la combinaison est fausse
                return;
            }
        }

        // Si la combinaison est correcte
        resultText.text = "Tu as gagné !";
    }

    // Réinitialise les chiffres et l'UI
    private void ResetNumbers()
    {
        for (int i = 0; i < chiffres.Count; i++)
        {
            chiffres[i] = 0; // Réinitialise chaque chiffre à 0
            uiTexts[i].text = "0"; // Met à jour l'UI
        }

        resultText.text = ""; // Réinitialise le message de résultat
    }
    

    public void CheckButton()
    {
        CheckCombination();
    }
    

}
