using UnityEngine;
using TMPro; // <- TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public TMP_Text scoreText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void RestarPuntaje(int cantidad)
    {
        score -= cantidad;
        if (score < 0)
            score = 0;

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Puntaje: " + score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

}
