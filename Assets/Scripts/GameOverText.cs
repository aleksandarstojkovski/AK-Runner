using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public Text gameOverTitle;
    public Text gameOverText;
    public GameObject fireworks;

    void Start()
    {
        float currentScore = PlayerPrefs.GetFloat(GamePrefs.Keys.CURRENT_SCORE);
        float recordScoreForCurrentMap = PlayerPrefs.GetFloat(GamePrefs.Keys.CURRENT_MAP_RECORD_SCORE);
        
        gameOverText.fontSize = 150;
        gameOverText.text = "Your score: " + currentScore + "\n" + "⋆ Best score for this map: " + recordScoreForCurrentMap + " ⋆";

        if (currentScore.Equals(recordScoreForCurrentMap)) {
            fireworks.SetActive(true);
            Messenger.Broadcast(GameEvent.PLAY_FIREWORKS, MessengerMode.DONT_REQUIRE_LISTENER);
            gameOverTitle.text = "New Record";
        } else
        {
            fireworks.SetActive(false);
            gameOverTitle.text = "Game Over";
        }
    }

    void Update()
    {
        
    }

}
