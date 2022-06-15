using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    // Recommence une nouvelle partie si on appuie sur le bouton "click to replay"
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
        GM.gameFinish = false;
    }

    // Quitte le jeu si on appuie sur le bouton "quit the game"
    public void QuitGame() => Application.Quit();
}
