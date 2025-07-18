using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score = 0;
    public bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UIController.Instance?.UpdateScore(score);
        Debug.Log("Score: " + score);
    }
    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    public void Win()
    {
        StartCoroutine(WinDelay());
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameWin");
    }


}
