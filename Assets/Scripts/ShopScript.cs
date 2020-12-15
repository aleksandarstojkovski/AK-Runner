using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{

    public float availableCoins;
    public bool isStreetMapBought;
    public Button buyStreetMapButton;
    private float streetMapPrice = 100;
    public Image soldImage;
    public Text streetMapPriceText;

    // Start is called before the first frame update
    void Start()
    {
        availableCoins = PlayerPrefs.GetFloat(GamePrefs.Keys.COINS_AMNT);
        isStreetMapBought = PlayerPrefs.GetInt(GamePrefs.Keys.SHOP_STREETMAP_BOUGHT, 0) == 1 ? true : false;
        streetMapPriceText.text = "Price: " + streetMapPrice + " coins";
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
    }

    public void buyStreetMap() {
        if (availableCoins >= streetMapPrice)
        {
            isStreetMapBought = true;
            PlayerPrefs.SetInt(GamePrefs.Keys.SHOP_STREETMAP_BOUGHT, 1);
        }
    }

}
