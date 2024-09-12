using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Pour gérer le changement de scène
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;


    private void Start()
    {
        AudioManager.instance.PlayMusicByState(SoundState.MENU);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
        AudioManager.instance.StopCurrentMusic();
        AudioManager.instance.PlayMusicByState(SoundState.MUSIC);


    }

    

    public void QuitGame()
    {
        Application.Quit();
    }
}
