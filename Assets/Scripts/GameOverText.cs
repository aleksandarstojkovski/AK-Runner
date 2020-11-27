using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{

    public GameObject fireworks;

    void Start()
    {
        Text gameOverText = GetComponent<Text>();
        float currentScore = PlayerPrefs.GetFloat(GamePrefs.Keys.CURRENT_SCORE);
        float recordScoreForCurrentMap = PlayerPrefs.GetFloat(GamePrefs.Keys.CURRENT_MAP_RECORD_SCORE);
        
        gameOverText.fontSize = 150;
        gameOverText.text = "Your score: " + currentScore + "\n" + "⋆ Best score for this map: " + recordScoreForCurrentMap + " ⋆";

        if (currentScore == recordScoreForCurrentMap) {
            fireworks.SetActive(true);
        }
    }

    void Update()
    {
        
    }

}
