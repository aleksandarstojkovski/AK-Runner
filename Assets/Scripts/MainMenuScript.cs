using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public void Start()
    {
        if (!PlayerPrefs.HasKey(GamePrefs.Keys.CURRENT_MAP_NAME))
        {
            // default map
            PlayerPrefs.SetString(GamePrefs.Keys.CURRENT_MAP_NAME, GamePrefs.Values.DESERT_MAP);
        }

        //Volume
        if (PlayerPrefs.HasKey(GamePrefs.Keys.CURRENT_GAME_VOLUME))
        {
            AudioListener.volume = PlayerPrefs.GetFloat(GamePrefs.Keys.CURRENT_GAME_VOLUME);
        }
    }

    public void PlayGame()
    {
        Messenger.Broadcast(GameEvent.STOP_FIREWORKS, MessengerMode.DONT_REQUIRE_LISTENER);
        SceneManager.LoadScene("Game" + PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME));
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Game" + PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME));
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToMainMenu()
    {
        Messenger.Broadcast(GameEvent.STOP_FIREWORKS, MessengerMode.DONT_REQUIRE_LISTENER);
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
