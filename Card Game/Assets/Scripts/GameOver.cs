using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    private TextMeshProUGUI _scoreText, _highscoreText;
    private Button _mainMenuButton;
    void Start()
    {
        _scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _highscoreText = transform.Find("HighscoreText").GetComponent<TextMeshProUGUI>();
        _mainMenuButton = transform.Find("MainMenuButton").GetComponent<Button>();
        _scoreText.text = "Score: "+PlayerPrefs.GetInt("Score");
        _highscoreText.text = "Personal Best: "+PlayerPrefs.GetInt("Highscore");
        _mainMenuButton.onClick.AddListener(GoToMainMenu);
    }
    void GoToMainMenu(){
        SceneManager.LoadScene("MenuScene");
    }
}
