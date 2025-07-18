using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;


    private void Start()
    {
        int score = GameManager.Instance.GetScore();
        finalScoreText.text = "Score: " + score.ToString();
    }
}
