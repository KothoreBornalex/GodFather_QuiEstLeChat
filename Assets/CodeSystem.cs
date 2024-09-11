using UnityEngine;
using TMPro;

public class CodeSystem : MonoBehaviour
{
    public TextMeshProUGUI uiText1; 
    public TextMeshProUGUI uiText2; 
    public TextMeshProUGUI uiText3;

    public TextMeshProUGUI resultText;

    private int chiffre1 = 0; 
    private int chiffre2 = 0; 
    private int chiffre3 = 0;


    public int targetNumber;

    public void UpButtonClickLeft()
    {
        Increment(ref chiffre1, uiText1);
    }

    public void UpButtonClickMiddle()
    {
        Increment(ref chiffre2, uiText2);
    }

    public void UpButtonClickRight()
    {
        Increment(ref chiffre3, uiText3);
    }
    public void DownButtonClickLeft()
    {
        Dencrement(ref chiffre1, uiText1);
    }

    public void DownButtonClickMiddle()
    {
        Dencrement(ref chiffre2, uiText2);
    }
    public void DownButtonClickRight()
    {
        Dencrement(ref chiffre3, uiText3);
    }

    private void Increment(ref int chiffre, TextMeshProUGUI uiText)
    {
        chiffre++;

        if (chiffre > 9)
        {
            chiffre = 0;
        }

        uiText.text = chiffre.ToString();
    }
    private void Dencrement(ref int chiffre, TextMeshProUGUI uiText)
    {
        chiffre--;

        if (chiffre < 0)
        {
            chiffre = 9;
        }

        uiText.text = chiffre.ToString();
    }

    public void CheckButton()
    {
        CheckCombination();
    }


    private void CheckCombination()
    {
        // Extraction des chiffres du nombre cible
        int hundreds = targetNumber / 100; // Premier chiffre (centaines)
        int tens = (targetNumber / 10) % 10; // Deuxième chiffre (dizaines)
        int units = targetNumber % 10; // Troisième chiffre (unités)

        // Si la combinaison est correcte
        if (chiffre1 == hundreds && chiffre2 == tens && chiffre3 == units)
        {
            resultText.text = "Tu as gagné !"; // Affiche le message de victoire
        }
        else
        {
            // Remet les chiffres à 0 si la combinaison est incorrecte
            ResetNumbers();
            resultText.text = "Code incorect !";
        }
    }

    private void ResetNumbers()
    {
        chiffre1 = 0;
        chiffre2 = 0;
        chiffre3 = 0;

        // Met à jour les textes dans l'UI
        uiText1.text = chiffre1.ToString();
        uiText2.text = chiffre2.ToString();
        uiText3.text = chiffre3.ToString();
    }

}
