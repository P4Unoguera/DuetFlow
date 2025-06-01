using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score1 = 0;
    public int score2 = 0;

    public GameObject player1Scorer;
    public GameObject player2Scorer;

    private float distancePerPoint = 0.00002f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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


    private void UpdateScorer(GameObject scorer, int score)
    {
        if (scorer.CompareTag("Player1"))
        {
            scorer.transform.position = scorer.transform.position - new Vector3(score1 * distancePerPoint, 0, 0);
        }

        if (scorer.CompareTag("Player2"))
        {
            scorer.transform.position = scorer.transform.position + new Vector3(score2 * distancePerPoint, 0, 0);
        }

    }
}