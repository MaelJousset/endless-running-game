using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.ScenemManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public int init = 1;

    // Update is called once per frame
    void Update()
    {
        int i = init;
        if (i==1)
        {
            //ferme a l'initialisation
            pauseMenuUI.SetActive(false);
            init = 0;
        }
        //change d'etat si appuie sur echap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) {Resume();}
            else {Pause();}
        }
    }

    //reprise du jeu
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //mise en pause
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //retourne sur le menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        Debug.Log("Menu game...");
    }

    //quitte le jeu
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
} 
