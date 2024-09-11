using UnityEngine;
using UnityEngine.SceneManagement;  // Pour g�rer le changement de sc�ne
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;


    private void Start()
    {
        AudioManager.instance.PlayRandom(SoundState.MENU);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
