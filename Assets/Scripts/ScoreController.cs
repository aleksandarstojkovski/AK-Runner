using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;

public class ScoreController : MonoBehaviour
{
    
    public Text currentScoreText;
    public Text currentCoinsText;
    public float recordScore;
    public static ScoreController Instance = null;
    public List<PlayerMetadata> ranking;
    public string initialString;


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
        ranking = new List<PlayerMetadata>();
        Messenger<PlayerMetadata>.AddListener(GameEvent.UPDATE_METADATA, UpdateStatusBar);
        Messenger<PlayerMetadata>.AddListener(GameEvent.STORE_RANKING, StoreRanking);
        Messenger<float>.AddListener(GameEvent.NEW_RECORD, NewRecord);

        initialString = PlayerPrefs.GetString("ranking");
        ranking = JsonConvert.DeserializeObject<List<PlayerMetadata>>(initialString) as List<PlayerMetadata>;
        if (ranking == null)
            ranking = new List<PlayerMetadata>();
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

    void StoreRanking(PlayerMetadata playerMetadata)
    {
        ranking.Add(playerMetadata);
        PlayerPrefs.SetString("ranking",JsonConvert.SerializeObject(ranking).ToString());
        Debug.Log(PlayerPrefs.GetString("ranking"));
    }

}
