using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.PlayerSettings;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public float score1 = 0;
    public float score2 = 0;

    public GameObject player1Scorer;
    public GameObject player2Scorer;

    private float distancePerPoint = 0.0002f;

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
        Debug.Log("Score1: " + score1);

        score1 += points;
        UpdateScorer(player1Scorer, score1);
    }

    public void AddScore2(int points)
    {
        Debug.Log("Score2: " + score2);

        score2 += points;
        UpdateScorer(player2Scorer, score2);
    }


    private void UpdateScorer(GameObject scorer, float score)
    {
        if (scorer.CompareTag("Player1") && scorer.transform.localPosition.x > -0.49f && scorer.transform.localPosition.x < 0.46f)
        {
            scorer.transform.localPosition = scorer.transform.localPosition - new Vector3(distancePerPoint, 0, 0);
        }

        if (scorer.CompareTag("Player2") && scorer.transform.localPosition.x > -0.46f && scorer.transform.localPosition.x < 0.49f)
        {
            scorer.transform.localPosition = scorer.transform.localPosition + new Vector3(distancePerPoint, 0, 0);
        }

    }
}