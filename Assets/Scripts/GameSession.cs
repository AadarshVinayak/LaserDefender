using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score;

    // Start is called before the first frame update
    void Awake()
    {
        StartSingleton();
    }

    private void StartSingleton()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int score)
    {
        this.score += score;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
