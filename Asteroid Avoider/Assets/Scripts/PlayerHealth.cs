using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameOverHandler _gameOverHandler;

    public void Crash()
    {
        gameObject.SetActive(false);
        _gameOverHandler.EndGame();
    }
}