using UnityEngine;
using UnityEngine.SceneManagement;


public class GUIButtonHandler : MonoBehaviour
{

    public GameObject menu;
    private bool SceneLoaded = false;
    private bool GamePaused = false;

    public void Update()
    {
        ShowPauseMenu();
    }

    public void loadGame()
    {
        //load level one
        //when I load this level I want the canvas to populate to the new level

        DontDestroyOnLoad(this.gameObject);
        menu.SetActive(false);
        SceneManager.LoadScene("DaScene");
        SceneLoaded = true;
    }

    public void exitGame()
    {
        //this only works on a full build, as in the game is built outside the Unity Editor
        Application.Quit();
    }

    private void ShowPauseMenu()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && SceneLoaded)
        {
            if (!GamePaused)
            {
                menu.SetActive(true);
                Time.timeScale = 0;
                GamePaused = true;
            }
            else
            {
                menu.SetActive(false);
                Time.timeScale = 1;
                GamePaused = false;
            }
        }

    }
}
