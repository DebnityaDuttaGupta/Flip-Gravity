using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timerDuration = 120f;
    private float timeRemaining;

    public TextMeshProUGUI timerText;
    public GameObject gameOverText;
    public GameObject winText;

    public PlayerMovement playerMovement;
    public ObjectCollectible objectCollectible;

    void Start()
    {
        timeRemaining = timerDuration;
        gameOverText.SetActive(false);
        winText.SetActive(false);
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            CheckGameOverConditions();
            timeRemaining = 0;
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CheckGameOverConditions()
    {
        bool isGrounded = playerMovement.grounded;
        bool collectAll = objectCollectible.collectedItems >= 5;

        if (isGrounded && collectAll)
        {
            winText.SetActive(true);
        }
        else
        {
            gameOverText.SetActive(true);
        }

        Time.timeScale = 0;
    }

    
}
