using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    private float elapsedTime = 0f;
    private int currentScore = 0;
    private WaitForSeconds updateInterval = new WaitForSeconds(1f); 

    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI scoreText2; 

    void Start()
    {
        StartCoroutine(UpdateScoreCoroutine());
        scoreText.text = "Score: " + 0;
        scoreText2.text = "Score: " + 0;
    }

    public void EndGame()
    {
        StopAllCoroutines();
    }

    IEnumerator UpdateScoreCoroutine()
    {
        while (true)
        {
            yield return updateInterval;

            if (elapsedTime <= 15f)
            {
                currentScore += 1;
            }
            else if (elapsedTime <= 40f)
            {
                currentScore += 2;
            }
            else if (elapsedTime <= 90f)
            {
                currentScore += 3;
            }
            else
            {
                currentScore += 5;
            }

            if (scoreText != null)
            {
                scoreText.text = "Score: " + currentScore.ToString();
                scoreText2.text = "Score: " + currentScore.ToString();
            }

            elapsedTime += 1f;
        }
    }
}
