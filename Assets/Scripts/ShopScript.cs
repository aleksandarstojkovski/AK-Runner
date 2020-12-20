using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{

    public bool isStreetMapBought;
    public Button buyStreetMapButton;
    private float streetMapPrice = 100;
    private float specialCoinPrice = 30;
    public Image soldImage;
    public Text streetMapPriceText;

    public Text specialCoinPriceText;
    public Text specialCoinNumberText;


    // Start is called before the first frame update
    void Start()
    {
        isStreetMapBought = PlayerPrefs.GetInt(GamePrefs.Keys.SHOP_STREETMAP_BOUGHT, 0) == 1 ? true : false;
        streetMapPriceText.text = "Price: " + streetMapPrice + " coins";
        specialCoinPriceText.text = "Price: " + specialCoinPrice + " coins";
        specialCoinNumberText.text = "x" + PlayerPrefs.GetInt(GamePrefs.Keys.SPECIAL_COIN_NUMBER);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStreetMapBought) {
            buyStreetMapButton.interactable = true;
            soldImage.enabled = false;
        }
        else
        {
            buyStreetMapButton.interactable = false;
            soldImage.enabled = true;
        }

        specialCoinNumberText.text = "x" + PlayerPrefs.GetInt(GamePrefs.Keys.SPECIAL_COIN_NUMBER);
    }

    public void buyStreetMap() {
        if (PlayerPrefs.GetFloat(GamePrefs.Keys.COINS_AMNT) >= streetMapPrice)
        {
            isStreetMapBought = true;
            PlayerPrefs.SetInt(GamePrefs.Keys.SHOP_STREETMAP_BOUGHT, 1);
            PlayerPrefs.SetFloat(GamePrefs.Keys.COINS_AMNT, PlayerPrefs.GetFloat(GamePrefs.Keys.COINS_AMNT) - streetMapPrice);
            Messenger.Broadcast(GameEvent.RELOAD_SCORE_CONTROLLER);
        }
    }

    public void buySpecialCoin()
    {
        if (PlayerPrefs.GetFloat(GamePrefs.Keys.COINS_AMNT) >= specialCoinPrice)
        {
            PlayerPrefs.SetInt(GamePrefs.Keys.SPECIAL_COIN_NUMBER, PlayerPrefs.GetInt(GamePrefs.Keys.SPECIAL_COIN_NUMBER) + 1);
            PlayerPrefs.SetFloat(GamePrefs.Keys.COINS_AMNT, PlayerPrefs.GetFloat(GamePrefs.Keys.COINS_AMNT) - specialCoinPrice);
            Messenger.Broadcast(GameEvent.RELOAD_SCORE_CONTROLLER);
        }
    }
}
