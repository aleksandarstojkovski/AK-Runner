using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetadata
{

    public string name;
    public string map;
    public float score;
    public float coins;

    public PlayerMetadata(float coins, float score)
    {
        this.coins = coins;
        this.score = score;
        this.name = PlayerPrefs.GetString(GamePrefs.Keys.PLAYER_NAME,"Unknown");
        this.map = PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME);
    }

    public void update(float coins, float score) {
        this.coins = coins;
        this.score = score;
        this.name = PlayerPrefs.GetString(GamePrefs.Keys.PLAYER_NAME);
        this.map = PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME);
    }

    public float getScore() {
        return score;
    }

    public float getCoins()
    {
        return coins;
    }

    public string getName()
    {
        return name;
    }

}
