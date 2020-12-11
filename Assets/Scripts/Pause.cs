using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] public static bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Messenger.Broadcast(GameEvent.UNPAUSE);
            } else
            {
                Messenger.Broadcast(GameEvent.PAUSE);
            }

            isPaused = !isPaused;

        }

        if (isPaused)
        {
            ActivateMenu();
        } else
        {
            DeactivateMenu();
        }

    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void Resume()
    {
        Messenger.Broadcast(GameEvent.PLAY_SETTINGS_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        DeactivateMenu();
        Messenger.Broadcast(GameEvent.UNPAUSE);
    }

    public void Quit()
    {
        Messenger.Broadcast(GameEvent.PLAY_SETTINGS_SOUND, MessengerMode.DONT_REQUIRE_LISTENER);
        DeactivateMenu();
        SceneManager.LoadScene("MainMenu");
    }

}