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
        currentMoneyText.text = "Coins: " + PlayerPrefs.GetFloat("coins").ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
