using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{

    void Start()
    {
        Text gameOverText = GetComponent<Text>();
        gameOverText.fontSize = 150;
        gameOverText.text = "Your score: " + PlayerPrefs.GetFloat(GamePrefs.Keys.CURRENT_SCORE) + "\n" + "⋆ Best score for this map: " + PlayerPrefs.GetFloat(GamePrefs.Keys.CURRENT_MAP_RECORD_SCORE) + " ⋆";
    }

    void Update()
    {
        
    }

}
