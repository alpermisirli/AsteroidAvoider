using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;
    private bool shouldCount = true;
    private float score;

    // Update is called once per frame
    void Update()
    {
        if (!shouldCount)
        {
            return;
        }

        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = "Score : " + Mathf.FloorToInt(score).ToString();
    }

    public void StartTimer()
    {
        shouldCount = true;
    }

    public int EndTimer()
    {
        shouldCount = false;

        scoreText.text = String.Empty;

        return Mathf.FloorToInt(score);
    }
}