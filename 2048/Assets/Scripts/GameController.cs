using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameController : MonoBehaviour
{
    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    
    [SerializeField] private Text gameResult;
    [SerializeField] private Text pointsText;

    [Inject] private Field Field;
    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        gameResult.text = "";
        
        SetPoints(0);
        GameStarted = true;
        
        Field.GenerateField();
    }

    public void Win()
    {
        GameStarted = false;
        gameResult.text = "You Win!";
    }

    public void Lose()
    {
        GameStarted = false;
        gameResult.text = "You Lose!";
    }

    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    public void SetPoints(int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }
}
