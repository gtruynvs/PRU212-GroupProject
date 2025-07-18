using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Start-Game");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game from menu.");
        Application.Quit();
    }
}
