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

        //Special Coin
        if (!PlayerPrefs.HasKey(GamePrefs.Keys.SPECIAL_COIN_NUMBER))
        {
            PlayerPrefs.SetInt(GamePrefs.Keys.SPECIAL_COIN_NUMBER, 0);
        }
    }

    public void PlayGame()
    {
        Messenger.Broadcast(GameEvent.STOP_FIREWORKS, MessengerMode.DONT_REQUIRE_LISTENER);
        Messenger.Broadcast(GameEvent.PLAY_BUTTON_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        SceneManager.LoadScene("Game" + PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME));
    }

    public void RetryGame()
    {
        Messenger.Broadcast(GameEvent.PLAY_BUTTON_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        SceneManager.LoadScene("Game" + PlayerPrefs.GetString(GamePrefs.Keys.CURRENT_MAP_NAME));
    }

    public void GoToSettingsMenu()
    {
        Messenger.Broadcast(GameEvent.PLAY_BUTTON_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToMainMenu()
    {
        Messenger.Broadcast(GameEvent.STOP_FIREWORKS, MessengerMode.DONT_REQUIRE_LISTENER);
        Messenger.Broadcast(GameEvent.PLAY_BUTTON_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToGameOverMenu()
    {
        Messenger.Broadcast(GameEvent.PLAY_BUTTON_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        SceneManager.LoadScene("GameOverMenu");
    }

    public void QuitGame()
    {
        Messenger.Broadcast(GameEvent.PLAY_BUTTON_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        Application.Quit();
    }

    public void Shop()
    {
        Messenger.Broadcast(GameEvent.PLAY_BUTTON_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        SceneManager.LoadScene("Shop");
    }

    public void Ranking()
    {
        Messenger.Broadcast(GameEvent.PLAY_BUTTON_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        SceneManager.LoadScene("Ranking");
    }
}
