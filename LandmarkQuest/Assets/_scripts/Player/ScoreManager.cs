using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int maxScore = 8; 
    private int score = 0;
    public bool isMaxScore = false;

    public GameObject maxScoreEvent;
    public GameObject timeManager;

    #region
    public static ScoreManager ScoreInstance;

    private void Awake()
    {
        if (ScoreInstance == null)
        {
            ScoreInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;

        if (score >= maxScore && !isMaxScore)
        {
            isMaxScore = true;
            if (maxScoreEvent != null)
            {
                maxScoreEvent.SetActive(true);
            }
            if (timeManager != null)
            {
                timeManager.SetActive(false);
            }
        }
    }
}
