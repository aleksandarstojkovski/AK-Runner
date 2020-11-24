using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] public static bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        Messenger.Broadcast(GameEvent.PAUSE);
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    public void DeactivateMenu()
    {
        Messenger.Broadcast(GameEvent.UNPAUSE);
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

}