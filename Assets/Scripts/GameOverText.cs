using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Text gameOverText = GetComponent<Text>();
        gameOverText.fontSize = 150;
        gameOverText.text = "Your score: " + Mathf.Round(PlayerPrefs.GetFloat("currentScore")) + "\n" + "⋆ Best score: " + Mathf.Round(PlayerPrefs.GetFloat("recordScore")) + " ⋆";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
