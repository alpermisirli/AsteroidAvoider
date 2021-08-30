using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;

    public void EndGame()
    {
        _asteroidSpawner.enabled = false;
        int finalScore = scoreSystem.EndTimer();
        gameOverText.text = "Your score is : " + finalScore.ToString();
        gameOverDisplay.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}