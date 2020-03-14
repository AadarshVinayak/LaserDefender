using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text gameScore;
    GameSession session;

    // Start is called before the first frame update
    void Start()
    {
        gameScore = GetComponent<Text>();
        session = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        gameScore.text = session.GetScore().ToString();   
    }
}
