using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueButton;
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

    public void ContinueGame()
    {
        scoreSystem.StartTimer();
        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _asteroidSpawner.enabled = true;
        gameOverDisplay.SetActive(false);
    }

    public void ContinueButton()
    {
        AdManager.Instance.ShowAd(this);
        continueButton.interactable = false;
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