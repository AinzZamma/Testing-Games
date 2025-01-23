using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ClickerGame : MonoBehaviour
{
    public Text scoreText;           
    public Button clickableButton;   
    public Button mainMenuButton;    
    public Button newGameButton;     

    private int score;

    void Start()
    {
        
        if (PlayerPrefs.HasKey("ClickerScore"))
        {
            score = PlayerPrefs.GetInt("ClickerScore");
            Debug.Log("Score loaded: " + score);
            UpdateScoreUI();
        }
        else
        {
            score = 0;
        }

        
        clickableButton.onClick.AddListener(OnImageClick);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        newGameButton.onClick.AddListener(StartNewGame);

        
        mainMenuButton.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        newGameButton.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    
    public void OnImageClick()
    {
        score++;  
        Debug.Log("OnImageClick triggered. Score: " + score);
        PlayerPrefs.SetInt("ClickerScore", score);  
        UpdateScoreUI();
    }

    
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;  
        }
        else
        {
            Debug.LogError("Score Text is null during UI update!");
        }
    }
    public void GoToGame2()
    {
        SceneManager.LoadScene("Game2");  
    }
   
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  
    }

    
    public void StartNewGame()
    {
        score = 0;  
        PlayerPrefs.SetInt("ClickerScore", score);  
        UpdateScoreUI();
        Debug.Log("New Game started.");
    }
}




