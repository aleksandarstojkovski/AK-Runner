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
        gameOverText.text = "Your score: " + PlayerPrefs.GetFloat("currentScore") + "\n" + "Best score: " + PlayerPrefs.GetFloat("recordScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
