using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score1 = 0;
    public int score2 = 0;

    public RectTransform player1Scorer;
    public RectTransform player2Scorer;

    private Vector3 score1StartPos;
    private Vector3 score2StartPos;

    public float distancePerPoint = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            score1StartPos = player1Scorer.anchoredPosition;
            score2StartPos = player2Scorer.anchoredPosition;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore1(int points)
    {
        score1 += points;
        UpdateScorer(player1Scorer, score1);
    }

    public void AddScore2(int points)
    {
        score2 += points;

        UpdateScorer(player2Scorer, score2);
    }


    private void UpdateScorer(RectTransform scorer, int score)
    {
        player1Scorer.anchoredPosition = score1StartPos + new Vector3(score1 * distancePerPoint, 0, 0);
        player2Scorer.anchoredPosition = score2StartPos - new Vector3(score2 * distancePerPoint, 0, 0);
    }
}