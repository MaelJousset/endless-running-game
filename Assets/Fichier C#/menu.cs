using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // Commence la partie si on appuie sur "Play"
    public void startGame() => SceneManager.LoadScene("Level1");
    // Quitte le jeu si on appuie sur "Quit"
    public void quitGame() => Application.Quit();
}
