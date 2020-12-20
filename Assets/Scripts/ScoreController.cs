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
    public string rankingJsonString;
    public string currentScoreLabelText = "Score: ";
    public string coinsLabelText = "Coins: ";

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
        switch (PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME))
        {
            case GamePrefs.Values.DESERT_MAP:
                PlayerPrefs.SetFloat(GamePrefs.Keys.CURRENT_MAP_RECORD_SCORE, PlayerPrefs.GetFloat(GamePrefs.Keys.DESERT_MAP_RECORD_SCORE,0));
                break;
            case GamePrefs.Values.STREET_MAP:
                PlayerPrefs.SetFloat(GamePrefs.Keys.CURRENT_MAP_RECORD_SCORE, PlayerPrefs.GetFloat(GamePrefs.Keys.STREET_MAP_RECORD_SCORE,0));
                break;
        }
    }

    void Start()
    {
        currentCoinsText = GameObject.Find("currentCoinsText").GetComponent<Text>();
        currentScoreText = GameObject.Find("currentScoreText").GetComponent<Text>();

        currentScoreText.text = currentScoreLabelText + 0;
        currentCoinsText.text = coinsLabelText + PlayerPrefs.GetFloat(GamePrefs.Keys.COINS_AMNT, 0);

        Messenger<PlayerMetadata>.AddListener(GameEvent.UPDATE_METADATA, UpdateStatusBar);
        Messenger<PlayerMetadata>.AddListener(GameEvent.STORE_RANKING, StoreRanking);
        Messenger.AddListener(GameEvent.RELOAD_SCORE_CONTROLLER, ReloadScore);

        rankingJsonString = PlayerPrefs.GetString(GamePrefs.Keys.RANKING_JSON,"[]");

        ranking = JsonConvert.DeserializeObject<List<PlayerMetadata>>(rankingJsonString) as List<PlayerMetadata>;

        if (ranking == null)
            // ranking empty
            ranking = new List<PlayerMetadata>();

    }

    void Update()
    {
        
    }

    void ReloadScore()
    {
        ranking.Clear();
        rankingJsonString = "";
        currentScoreText.text = currentScoreLabelText + 0;
        currentCoinsText.text = coinsLabelText + PlayerPrefs.GetFloat(GamePrefs.Keys.COINS_AMNT, 0);
    }

    void UpdateStatusBar(PlayerMetadata playerMetadata) {
        float score = playerMetadata.getScore();
        float coins = playerMetadata.getCoins();
        currentScoreText.text = currentScoreLabelText + score.ToString();
        currentCoinsText.text = coinsLabelText + coins.ToString();
        PlayerPrefs.SetFloat(GamePrefs.Keys.CURRENT_SCORE, score);
        PlayerPrefs.SetFloat(GamePrefs.Keys.COINS_AMNT, coins);
    }

    void StoreRanking(PlayerMetadata playerMetadata)
    {
        switch (PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME))
        {
            case GamePrefs.Values.DESERT_MAP:
                if (playerMetadata.getScore() > PlayerPrefs.GetFloat(GamePrefs.Keys.DESERT_MAP_RECORD_SCORE, 0)) {
                    // new record for Desert map
                    PlayerPrefs.SetFloat(GamePrefs.Keys.DESERT_MAP_RECORD_SCORE, playerMetadata.score);
                    PlayerPrefs.SetFloat(GamePrefs.Keys.CURRENT_MAP_RECORD_SCORE, playerMetadata.score);
                }
                break;
            case GamePrefs.Values.STREET_MAP:
                if (playerMetadata.getScore() > PlayerPrefs.GetFloat(GamePrefs.Keys.STREET_MAP_RECORD_SCORE, 0))
                {
                    // new record for Desert map
                    PlayerPrefs.SetFloat(GamePrefs.Keys.STREET_MAP_RECORD_SCORE, playerMetadata.score);
                    PlayerPrefs.SetFloat(GamePrefs.Keys.CURRENT_MAP_RECORD_SCORE, playerMetadata.score);
                }
                break;
        }
        ranking.Add(playerMetadata);
        PlayerPrefs.SetString(GamePrefs.Keys.RANKING_JSON, JsonConvert.SerializeObject(ranking).ToString());
    }

}
