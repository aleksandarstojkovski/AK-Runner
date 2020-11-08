using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Text currentMoneyText;

    public void Start()
    {
        //currentMoneyText.text = "Coins: " + PlayerPrefs.GetFloat("coins").ToString();
        if (!PlayerPrefs.HasKey("map"))
        {
            PlayerPrefs.SetString("map", "Desert");
        }
    }

    public void PlayGame()
    {
        Debug.Log("Caricato mappa " + PlayerPrefs.GetString("map"));
        SceneManager.LoadScene("Game" + PlayerPrefs.GetString("map"));
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("GameDesert");
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("NewMainMenu");
    }

    public void GoToGameOverMenu()
    {
        SceneManager.LoadScene("GameOverMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Ranking()
    {
        SceneManager.LoadScene("Ranking");
    }
}
