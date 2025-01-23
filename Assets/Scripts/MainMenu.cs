using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    public Button goToGame2Button;

    void Start()
    {
        playButton.onClick.AddListener(() => SceneManager.LoadScene("Game1"));
        exitButton.onClick.AddListener(Application.Quit);
        goToGame2Button.onClick.AddListener(() => SceneManager.LoadScene("Game2"));

        // Анимация кнопок
        playButton.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        goToGame2Button.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}


