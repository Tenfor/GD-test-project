using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private Button _startButton;
    private TextMeshProUGUI _highscoreText;
    void Start()
    {
        _startButton = transform.Find("StartButton").GetComponent<Button>();
        _startButton.onClick.AddListener(StartGame);
        _highscoreText = transform.Find("HighscoreText").GetComponent<TextMeshProUGUI>();
        int hs = PlayerPrefs.HasKey("Highscore") ?  PlayerPrefs.GetInt("Highscore") : 0;
        _highscoreText.text = "Personal Best: "+hs;
    }
    void StartGame(){
        SceneManager.LoadScene("GameScene");
    }
}