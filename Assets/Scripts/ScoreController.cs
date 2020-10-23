using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreController : MonoBehaviour
{
    
    public Text currentScoreText;
    public Text currentCoinsText;
    public float recordScore;
    public static ScoreController Instance = null;


    void Awake()
    {
        // singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
 


    // Start is called before the first frame update
    void Start()
    {
        currentCoinsText = GameObject.Find("currentCoinsText").GetComponent<Text>();
        currentScoreText = GameObject.Find("currentScoreText").GetComponent<Text>();
        recordScore = Mathf.Round(PlayerPrefs.GetFloat("recordScore"));
        Messenger<PlayerMetadata>.AddListener(GameEvent.UPDATE_METADATA, UpdateStatusBar);
        Messenger<float>.AddListener(GameEvent.NEW_RECORD, NewRecord);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateStatusBar(PlayerMetadata playerMetadata) {
        float score = Mathf.Round(playerMetadata.getScore());
        float coins = playerMetadata.getCoins();
        currentScoreText.text = "Score: " + score.ToString();
        currentCoinsText.text = "Coins: " + coins.ToString();
        PlayerPrefs.SetFloat("currentScore", score);
        PlayerPrefs.SetFloat("coins", coins);
    }

    void NewRecord(float newRecord)
    {
        PlayerPrefs.SetFloat("recordScore", newRecord);
    }

}
