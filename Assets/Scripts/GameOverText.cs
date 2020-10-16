using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text testo = GetComponent<Text>();
        testo.text = PlayerPrefs.GetString("GOScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
