using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetadata
{

    public string name;
    public float score;
    public float coins;

    public PlayerMetadata(float coins, float score, string name)
    {
        this.coins = coins;
        this.score = score;
        this.name = name;
    }

    public void update(float coins, float score, string name) {
        update(coins, score);
        this.name = name;
    }

    public void update(float coins, float score)
    {
        this.coins = coins;
        this.score = score;
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
