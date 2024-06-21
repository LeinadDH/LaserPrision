using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameObject _endGameCanvas;
    private int currentLives;

    public TMP_Text livesText;

    void Start()
    {
        currentLives = _maxLives;
        UpdateLivesUI();
        _endGameCanvas.SetActive(false);
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + currentLives.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            TakeDamage();

            other.gameObject.SetActive(false);

            if (currentLives <= 0)
            {
               _scoreManager.EndGame();
               _playerController.enabled = false;
                _endGameCanvas.SetActive(true);
            }
        }
    }

    void TakeDamage()
    {
        currentLives--;

        UpdateLivesUI();
    }
}
