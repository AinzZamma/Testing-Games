using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriPeaksGame : MonoBehaviour
{
    public GameObject cardPrefab; // ������ �����
    public Transform cardParent; // �������� ��� ����
    public Text timerText; // ����� ��� �������
    public Text moveText; // ����� ��� �����
    public Text statusText; // ����� ������� ���� (Win/Lose)
    public Button restartButton; // ������ ��������
    public Sprite[] cardSprites; // ������ �������� ����

    private List<Card> deck; // ������ ����
    private List<Card> tableCards; // ����� �� �����
    private Card activeCard; // �������� �����

    private int moves; // ���������� �����
    private float timer; // ������
    private bool isGameActive; // ��������� ����

    void Start()
    {
        isGameActive = true;
        moves = 0;
        timer = 0;
        statusText.text = "";
        restartButton.onClick.AddListener(RestartGame);

        if (cardPrefab == null || cardParent == null || cardSprites.Length == 0)
        {
            Debug.LogError("cardPrefab, cardParent ��� cardSprites �� ���������!");
            return;
        }

        CreateDeck();
        ShuffleDeck();
        SetupTable();
    }

    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = "Time: " + timer.ToString("F2");
            }
        }
    }

    void CreateDeck()
    {
        deck = new List<Card>();

        for (int i = 0; i < 52; i++)
        {
            int cardValue = (i % 13) + 1; // �������� ����� �� 1 �� 13
            Sprite cardSprite = cardSprites[cardValue - 1];

            GameObject cardObject = Instantiate(cardPrefab, cardParent);
            Card card = cardObject.GetComponent<Card>();
            card.InitializeCard(cardValue, cardSprite);
            cardObject.SetActive(false); // �������� ����� �� ���������
            deck.Add(card);
        }
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            var temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    void SetupTable()
    {
        tableCards = new List<Card>();
        float startX = -3f;
        float startY = 2f;
        int[] rows = { 4, 3, 2, 1 };
        int cardIndex = 0;

        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row]; col++)
            {
                Card card = deck[cardIndex];
                cardIndex++;

                if (card == null) continue; // ���� ����� �� �������

                card.transform.position = new Vector3(startX + col * 1.5f + row * 0.75f, startY - row * 2f, 0);
                card.gameObject.SetActive(true);

                card.SetInteractable(row == rows.Length - 1);
                tableCards.Add(card);
            }
        }

        if (deck.Count > cardIndex)
        {
            activeCard = deck[cardIndex];
            deck.RemoveAt(cardIndex);
            activeCard.transform.position = new Vector3(0, -4, 0);
            activeCard.gameObject.SetActive(true);
        }
    }

    public void OnCardClick(Card clickedCard)
    {
        if (!isGameActive || clickedCard == null || !clickedCard.IsInteractable) return;

        if (Mathf.Abs(activeCard.Value - clickedCard.Value) == 1)
        {
            activeCard.SetCard(clickedCard.Value, clickedCard.Sprite);

            tableCards.Remove(clickedCard);
            Destroy(clickedCard.gameObject);

            moves++;
            moveText.text = "Moves: " + moves;

            CheckWinCondition();
        }
    }

    void CheckWinCondition()
    {
        if (tableCards.Count == 0)
        {
            EndGame(true);
        }
        else if (deck.Count == 0)
        {
            EndGame(false);
        }
    }

    void EndGame(bool win)
    {
        isGameActive = false;
        statusText.text = win ? "You Win!" : "Game Over!";
    }

    public void RestartGame()
    {
        foreach (var card in tableCards) Destroy(card.gameObject);
        foreach (var card in deck) Destroy(card.gameObject);

        Start();
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void GoToGame1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game1");
    }
}








