using UnityEngine;
using TMPro;
[CreateAssetMenu(fileName = "UIInfoData", menuName = "ScriptableObjects/UIInfoData", order = 1)]
public class TestForJM : ScriptableObject
{
    // Image for the UI
    public Sprite image;

    // Name text
    public string nameText;

    // Description text
    public string descriptionText;
}
